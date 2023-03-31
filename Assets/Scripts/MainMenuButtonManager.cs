using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonManager : MonoBehaviour
{
    public void GoToLevelSelect()
    {
        SceneManager.LoadScene("Scenes/LevelSelect");
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("Scenes/Credits");
    }

    public void QuitTheGame()
    {
        Application.Quit();
    }
}
