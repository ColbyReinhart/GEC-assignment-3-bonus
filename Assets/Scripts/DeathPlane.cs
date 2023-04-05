using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles death plane functionality
public class DeathPlane : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager.instance.GameOver();
        }
        else if (other.CompareTag("BossProjectile"))
        {
            Destroy(other.gameObject);
        }
    }
}
