using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// This script manages all UI elements. This class should only be used by
// the level manager script.
public class UIManager : MonoBehaviour
{
    // Singleton reference
    public static UIManager instance;

    // UI elements
    public TMP_Text timer;
    public TMP_Text score;
    public TMP_Text gameOverText;
    public Animator fadeScreenAnim;

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

    public void UpdateScore(int value)
    {
        score.text = "Score: " + value;
    }

    public IEnumerator DoGameOverUI()
    {
        gameOverText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        fadeScreenAnim.SetBool("FadeOut", true);
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
