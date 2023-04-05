using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handle shooting with the gun
public class GunShooting : MonoBehaviour
{
    public Rigidbody player;
    public GameObject bulletPrefab;
    public Transform fireTransform;
    public AudioClip bulletShoot;
    public AudioClip blastShoot;
    public ParticleSystem bulletParticles;
    public ParticleSystem blastParticles;

    public bool canFire = true;
    public float bulletVelocity = 10f;
    public float bulletCooldown = 0.25f;
    public float blastForce = 250f;
    public float blastKnockback = 250f;
    public float blastCooldown = 1f;
    public float blastConeDistance = 10f;
    public float blastConeAngle = 30f;
    public LayerMask pushableLayerMask;

    private AudioSource gunAudio;
    private Collider playerCollider;
    private Animator animator;
    private float timeToNextBullet = 0f;
    private float timeToNextBlast = 0f;

    private void Awake()
    {
        gunAudio = GetComponent<AudioSource>();
        playerCollider = player.GetComponent<Collider>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        // Make sure we start off not aiming
        animator.SetBool("isAiming", false);
    }

    // Update is called once per frame
    private void Update()
    {
        // Count cooldowns
        if (timeToNextBullet > 0)
        {
            timeToNextBullet -= Time.deltaTime;
        }
        if (timeToNextBlast > 0)
        {
            timeToNextBlast -= Time.deltaTime;
        }

        if (!canFire)
        {
            return;
        }

        // Check if the user is aiming
        if (Input.GetButtonDown("Aim"))
        {
            animator.SetBool("isAiming", true);
        }
        else if (Input.GetButtonUp("Aim"))
        {
            animator.SetBool("isAiming", false);
        }

        // Check if the user is firing normal bullets
        if (Input.GetButtonDown("Fire1"))
        {
            bulletParticles.Play();
        }
        else if (Input.GetButton("Fire1") && timeToNextBullet <= 0f)
        {
            FireBullet();
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            bulletParticles.Stop();
        }
        
        // Check if the user is firing blasts
        if (Input.GetButtonDown("Fire2") && timeToNextBlast <= 0f)
        {
            FireBlast();
            blastParticles.Play();
        }
    }


    private void FireBullet()
    {
        // Shoot the bullet
        GameObject bullet = Instantiate(bulletPrefab, fireTransform);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletVelocity;
        Physics.IgnoreCollision(playerCollider, bullet.GetComponent<Collider>());

        // Set cooldown
        timeToNextBullet = bulletCooldown;

        // Play shooting sound
        gunAudio.volume = 0.5f;
        gunAudio.PlayOneShot(bulletShoot);
    }

    private void FireBlast()
    {
        // Get all pushable colliders in range
        foreach (Collider collider in Physics.OverlapSphere(transform.position, blastConeDistance, pushableLayerMask))
        {
            // Don't apply force to the player
            if (collider.gameObject.tag == "Player")
            {
                continue;
            }

            // Then check if each collider is inside the cone
            Vector3 direction = (collider.transform.position - transform.position).normalized;
            float dotProduct = Vector3.Dot(transform.forward, direction) * Mathf.Rad2Deg;
            if (Mathf.Abs(dotProduct) <= blastConeAngle)
            {
                // Apply force to all colliders in the blast cone
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(direction * blastForce);
                }
            }
        }

        // Give the player some knockback
        player.AddForce(Camera.main.transform.forward * -1 * blastKnockback);

        // Set cooldown
        timeToNextBlast = blastCooldown;

        // Play blast sound
        gunAudio.volume = 1f;
        gunAudio.PlayOneShot(blastShoot);
    }
}
