using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This script manages all UI elements
public class UIManager : MonoBehaviour
{
    // Singleton reference
    public static UIManager instance;

    // UI elements
    public TMP_Text timer;
    public TMP_Text score;

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

    private void Start()
    {
        // Initialize timer
        timer.text = LevelManager.instance.timeToComplete.ToString();
    }

    private void Update()
    {
        // Tick down timer
        int timeLeft = (int)(LevelManager.instance.timeToComplete - Time.time);
        timer.text = timeLeft.ToString();
    }
}
