using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    private AudioSource impactSound;
    private bool hasHit;

    private void Awake()
    {
        impactSound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasHit)
        {
            impactSound.Play();
            hasHit = true;
        }
    }
}
