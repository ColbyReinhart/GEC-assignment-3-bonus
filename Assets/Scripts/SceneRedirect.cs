using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRedirect : MonoBehaviour
{
    public string scenePath = "MainMenu";

    private void Start()
    {
        SceneManager.LoadScene("Scenes/" + scenePath);
    }
}
