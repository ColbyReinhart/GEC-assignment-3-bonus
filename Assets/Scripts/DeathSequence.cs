using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSequence : MonoBehaviour
{
    private ParticleSystem explosion;
    private AudioSource deathAudio;

    private void Awake()
    {
        explosion = GetComponent<ParticleSystem>();
        deathAudio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(DoDeathSequence());
    }

    IEnumerator DoDeathSequence()
    {
        deathAudio.Play();
        yield return new WaitForSeconds(1f);
        explosion.Play();
        yield return new WaitForSeconds(10f);
        deathAudio.Stop();
        LevelManager.instance.LevelComplete();
    }
}
