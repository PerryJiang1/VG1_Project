using UnityEngine;
using TMPro;
using CharacterMovement;
using UnityEngine.SceneManagement;
using System.Collections;


public class LevelComplete : MonoBehaviour
{

    public int levelNumber;
    public TMP_Text levelCompleteText;
    public RobotController robot;
    public string nextScene;



    private void Start()
    {
        // Hide "Level 1 Complete"
        levelCompleteText.gameObject.SetActive(false);
    }

    public void CompleteLevel(int levelNumber)
    {
        GameProgressManager.CompleteLevel(levelNumber);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CompleteLevel(levelNumber);
            // Display level complete info
            levelCompleteText.text = "Level " + levelNumber + " Complete!";
            levelCompleteText.gameObject.SetActive(true);

            // Disable player control
            robot.DisableControl();

            StartCoroutine(LoadNextSceneAfterDelay(2f));
        }
    }

    private IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(nextScene);
    }
}


