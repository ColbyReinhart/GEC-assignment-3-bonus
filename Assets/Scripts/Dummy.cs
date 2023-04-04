using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls dummy functionality
public class Dummy : MonoBehaviour
{
    public GameObject explosion;
    
    [SerializeField]
    private int worth; // How many points for knocking them over?
    private bool isDead = false;

    private void OnTriggerEnter(Collider other)
    {
        // If we hit terrain and we're still alive
        if (other.CompareTag("Terrain") && !isDead)
        {
            isDead = true;
            Destroy(gameObject);
            GameObject spawnedExplosion =
                Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(spawnedExplosion, 2f);
            LevelManager.instance.AddPoints(worth);
        }
    }
}
