using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handle shooting with the gun
public class GunShooting : MonoBehaviour
{
    public CharacterController character;
    public GameObject bulletPrefab;
    public Transform fireTransform;
    public AudioClip bulletShoot;
    public AudioClip blastShoot;

    public float bulletVelocity = 10f;
    public float bulletCooldown = 0.25f;
    public float blastForce = 10f;
    public float blastKnockback = 100000f;
    public float blastCooldown = 1f;
    public float blastConeDistance = 10f;
    public float blastConeAngle = 30f;

    private AudioSource gunAudio;
    private float timeToNextBullet = 0f;
    private float timeToNextBlast = 0f;
    private int pushableLayerMask = 3;

    private void Awake()
    {
        gunAudio = GetComponent<AudioSource>();
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

        // Check for user input
        if (Input.GetButton("Fire1") && timeToNextBullet <= 0f)
        {
            FireBullet();
        }
        
        if (Input.GetButtonDown("Fire2") && timeToNextBlast <= 0f)
        {
            FireBlast();
        }
    }


    private void FireBullet()
    {
        // Shoot the bullet
        GameObject bullet = Instantiate(bulletPrefab, fireTransform);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletVelocity;

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
            // Then check if each collider is inside the cone
            Vector3 direction = (collider.transform.position - transform.position).normalized;
            float dotProduct = Vector3.Dot(transform.forward, direction);
            if (dotProduct <= blastConeAngle)
            {
                // Apply force to all colliders in the blast cone
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(transform.forward * blastForce);
                }
            }
        }

        // Give the player some knockback
        Debug.Assert(character.attachedRigidbody != null);
        character.attachedRigidbody.AddForce(transform.forward * -1 * blastKnockback);

        // Set cooldown
        timeToNextBlast = blastCooldown;

        // Play blast sound
        gunAudio.volume = 1f;
        gunAudio.PlayOneShot(blastShoot);
    }
}
