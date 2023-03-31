using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Control the splash screen sequence
public class SplashScreenSequence : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DoSequence());
    }

    private IEnumerator DoSequence()
    {   
        // Using yield return here is apparently an evil hack.
        // As much as I hate being evil, it works and I don't have
        // a ton of time to figure out what's so wrong with it and
        // how to do it better.
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("Scenes/MainMenu");
    }
}
