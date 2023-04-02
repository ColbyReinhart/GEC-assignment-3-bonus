using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles bullet functionality
// Bullets are destroyed if their lifetime expires or they hit their target
public class Bullet : MonoBehaviour
{
    public float lifeTime = 3f;

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
