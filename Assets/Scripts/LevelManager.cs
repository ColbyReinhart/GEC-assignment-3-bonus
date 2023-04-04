using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script manages the game flow and controls level progression
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float timeToComplete = 150;

    private PlayerMovement player;
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

    private void Start()
    {
        // Get components
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    public void AddPoints(int value)
    {
        points += value;
        UIManager.instance.UpdateScore(points);
    }

    public void LevelComplete()
    {
        player.canMove = false;
        StartCoroutine(UIManager.instance.DoLevelClearUI());
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

    public void NextLevel()
    {
        // Get the current scene
        string sceneName = SceneManager.GetActiveScene().name;

        // Get the level number
        int levelNumber = sceneName[sceneName.Length() - 1];
    }
}
