using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsSequence : MonoBehaviour
{
    public float scrollSpeed = 100; // Unity units per second

    private GameObject credits;
    private Vector3 creditsTranslation = Vector3.zero;
    private float calculatedCreditsTime;

    private void Start()
    {
        // We'll figure out how tall the entire credits sequence is. From there, we can
        // use scrollSpeed to calculate when the credits will end and when we should load
        // the next scene. This logic assumes that the credits are originally positioned
        // just below the camera.

        // Get credits objects
        credits = GameObject.Find("Credits");
        GameObject creditsTitle = credits.transform.Find("CreditsTitle").gameObject;
        GameObject creditsNames = credits.transform.Find("Names").gameObject;

        // Get the total height of the credits
        float creditsTitleHeight = creditsTitle.GetComponent<RectTransform>().rect.height;
        float creditsNamesHeight = creditsNames.GetComponent<RectTransform>().rect.height;
        float creditsTotalHeight = creditsTitleHeight + creditsNamesHeight;

        // Calculate credits scroll time
        float totalDistanceToCover = Screen.height + creditsTotalHeight;
        calculatedCreditsTime = totalDistanceToCover / scrollSpeed;

        // Start the scene change coroutine
        StartCoroutine(GoToMainMenu());
    }

    private void Update()
    {
        creditsTranslation.y = scrollSpeed * Time.deltaTime;
        credits.transform.Translate(creditsTranslation);
    }

    private IEnumerator GoToMainMenu()
    {
        yield return new WaitForSeconds(calculatedCreditsTime);
        SceneManager.LoadScene("Scenes/MainMenu");
    }
}
