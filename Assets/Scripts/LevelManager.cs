using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script manages the game flow and controls level progression
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float timeToComplete = 150;

    private int points = 0;

    private void Awake()
    {
        // Setup singleton instance, but let it reset itself on scene reloads
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
    }

    public void AddPoints(int value)
    {
        points += value;
        UIManager.instance.UpdateScore(points);
    }

    public void GameOver()
    {
        // DoGameOverUI() will call Restart(), even though this class
        // should do it, because I need to learn how coroutines work
        // and I ran out of time.
        StartCoroutine(UIManager.instance.DoGameOverUI());
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
