using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene loading
using UnityEngine.UI;

public class StartButtonHandler : MonoBehaviour
{
    public Button startButton; // Reference to the button

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
        SceneManager.LoadScene("Level1"); // Replace "GameScene" with your actual scene name
    }
}
