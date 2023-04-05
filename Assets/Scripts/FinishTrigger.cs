using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Colliding with the associated collider/trigger will finish the level
public class FinishTrigger : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        LevelManager.instance.LevelComplete();
    }

    private void OnTriggerEnter(Collider other)
    {
        LevelManager.instance.LevelComplete();
    }
}
