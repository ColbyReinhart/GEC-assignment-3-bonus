using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject bossProjectile;
    public GameObject projectileTransform;
    public int health = 500;
    public float projectileSpeed = 10f;
    public float turnDelay = 3f;
    public float defeatedMass = 10f;

    private GameObject player;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void StartFight(GameObject playerObj)
    {
        player = playerObj;
        StartCoroutine(Fight());
    }

    IEnumerator Fight()
    {
        while (true)
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

            // Wait to do it again
            yield return new WaitForSeconds(turnDelay);
        }
    }
}
