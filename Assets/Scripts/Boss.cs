using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public GameObject bossProjectile;
    public GameObject projectileTransform;
    public GameObject canvas;
    public GameObject deathSequence;
    public Slider healthBar;
    public int health = 500;
    public float projectileSpeed = 10f;
    public float turnDelay = 3f;
    public float defeatedMass = 10f;

    private GameObject player;
    private AudioSource throwSound;
    private Rigidbody rb;
    private bool isDead = false;
    private bool isExploded = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        throwSound = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // Setup healthbar
        healthBar.maxValue = health;
        healthBar.value = health;
    }

    public void StartFight(GameObject playerObj)
    {
        canvas.SetActive(true);
        player = playerObj;
        StartCoroutine(Fight());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Terrain") && !isExploded)
        {
            isExploded = true;
            Instantiate(deathSequence, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            health--;
            healthBar.value = health;

            if (health <= 0)
            {
                rb.mass = defeatedMass;
                isDead = true;
            }
        }
    }

    IEnumerator Fight()
    {
        while (!isDead)
        {
            // Calculate projectile trajectory and get starting position
            Vector3 startPosition = projectileTransform.transform.position;
            Vector3 trajectory =
                player.transform.position - startPosition;
            trajectory = trajectory.normalized * projectileSpeed;

            // Spawn projectile
            GameObject projectile =
                Instantiate(bossProjectile, startPosition, Random.rotation);
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

            // Launch the projectile at the player
            projectileRb.velocity = trajectory;
            Vector3 angularVel = new Vector3
            (
                Random.Range(-5, 5),
                Random.Range(-5, 5),
                Random.Range(-5, 5)
            );
            projectileRb.angularVelocity = angularVel;
            throwSound.Play();

            // Wait to do it again
            yield return new WaitForSeconds(turnDelay);
        }
    }
}
