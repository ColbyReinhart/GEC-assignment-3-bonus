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

    // Level clear panel
    public GameObject levelClearPanel;
    public TMP_Text endingTimeLeft;
    public TMP_Text endingScore;
    public TMP_Text endingTotal;
    public Button nextLevelButton;

    private bool timerTicking = true;

    private void Awake()
    {
        // Setup singleton instance, but let it reset itself on scene reloads
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
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
        yield return new WaitForSeconds(2.5f);
        LevelManager.instance.Restart();
    }

    public IEnumerator DoLevelClearUI()
    {
        // Prelim
        timerTicking = false;
        int timeLeft = (int)(LevelManager.instance.timeToComplete - Time.timeSinceLevelLoad);

        // Activate the level clear panel
        levelClearPanel.SetActive(true);
        yield return new WaitForSeconds(1f);

        // Do ending time left
        endingTimeLeft.text = "Time left: " + timeLeft + " x 100";
        endingTimeLeft.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);

        // Do ending score
        int points = LevelManager.instance.GetPoints();
        endingScore.text = "Score: " + points;
        endingScore.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);

        // Do ending total
        int totalScore = (timeLeft * 100) + points;
        endingTotal.text = "Total: " + totalScore;
        endingTotal.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);

        // Do next level button
        nextLevelButton.gameObject.SetActive(true);
    }

    private void Start()
    {
        // Initialize timer
        timer.text = LevelManager.instance.timeToComplete.ToString();
    }

    private void Update()
    {
        // Tick down timer
        if (timerTicking)
        {
            int timeLeft = (int)(LevelManager.instance.timeToComplete - Time.timeSinceLevelLoad);
            timer.text = timeLeft.ToString();
        }
    }
}
