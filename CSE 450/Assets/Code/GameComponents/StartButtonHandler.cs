using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene loading
using UnityEngine.UI;
using System.Collections;


public class StartButtonHandler : MonoBehaviour
{
    public Button startButton; // Reference to the button
    public CanvasGroup canvasGroup; // Reference to the Canvas Group
    public float fadeDuration = 1f; // Duration of the fade effect in seconds
    public string nextSceneName = "Level1"; // Name of the scene to transition to
    void Start()
    {
        // Assign the click event listener to the button
        startButton.onClick.AddListener(OnStartButtonClick);
    }

    void OnStartButtonClick()
    {
        // Logic to handle the start button click
        Debug.Log("Start button clicked!");

        // Load the next scene (make sure to add the game scene in Build Settings)
        StartCoroutine(FadeAndLoadScene());
        // SceneManager.LoadScene("Level1"); // Replace "GameScene" with your actual scene name
    }
     private IEnumerator FadeAndLoadScene()
    {
        // Fade out the canvas group alpha over time
        float startTime = Time.time;
        while (Time.time < startTime + fadeDuration)
        {
            canvasGroup.alpha = 1 - (Time.time - startTime) / fadeDuration;
            yield return null;
        }

        // Ensure the canvas is fully transparent
        canvasGroup.alpha = 0;

        // Load the next scene (Level1)
        SceneManager.LoadScene(nextSceneName);
    }
}
