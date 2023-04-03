using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script manages the game flow and controls level progression
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float timeToComplete = 150;

    private int points = 0;

    private void Awake()
    {
        // Setup singleton instance
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void AddPoints(int value)
    {
        points += value;
        UIManager.instance.UpdateScore(points);
    }
}
