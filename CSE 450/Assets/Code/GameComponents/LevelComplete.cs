using UnityEngine;
using TMPro;
using CharacterMovement;

public class LevelComplete : MonoBehaviour
{
    public int levelNumber;
    public TMP_Text levelCompleteText;
    public RobotController robot;

    private void Start()
    {
        // Hide "Level 1 Complete"
        levelCompleteText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Display level complete info
            levelCompleteText.text = "Level " + levelNumber + " Complete!";
            levelCompleteText.gameObject.SetActive(true);

            // Disable player control
            robot.DisableControl();
        }
    }
}


