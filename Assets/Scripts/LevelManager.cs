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
    private AudioSource levelMusic;
    private int points = 0;

    private void Awake()
    {
        // Setup singleton instance, but let it reset itself on scene reloads
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;

        // Get components
        levelMusic = GetComponent<AudioSource>();
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

    public int GetPoints()
    {
        return points;
    }

    public void LevelComplete()
    {
        player.canMove = false;
        Cursor.lockState = CursorLockMode.None;
        levelMusic.Stop();
        StartCoroutine(UIManager.instance.DoLevelClearUI());
    }

    public void GameOver()
    {
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

        // Get the level number and increment by 1
        int levelNumber = sceneName[sceneName.Length - 1] + 1;

        // Load the next level
        SceneManager.LoadScene("Scenes/Levels/Level" + levelNumber);
    }
}
