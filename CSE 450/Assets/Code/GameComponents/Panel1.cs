using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CharacterMovement;

public class Panel1 : MonoBehaviour
{
    public Image panelImage;
    public AnimationCurve showCurve;
    public float animationSpeed;
    public string mainMenuScene;
    public string collectionScene;
    public string exitScene;
    public CharacterMovement.RobotController1 robotController;


    public GameObject[] buttons; 
    private bool isPaused = false; 
    private bool isPanelVisible = false; 



    IEnumerator ShowPanel()
    {
        float timer = 0;
        SetButtonsActive(true); 

        while (panelImage.color.a < 1)
        {
            panelImage.color = new Vector4(1, 1, 1, showCurve.Evaluate(timer));
            timer += Time.deltaTime * animationSpeed;
            yield return null;
        }
        Time.timeScale = 0; 
        isPaused = true; 
        isPanelVisible = true; 
    }

    IEnumerator HidePanel()
    {
        float timer = 0;
        while (panelImage.color.a > 0)
        {
            panelImage.color = new Vector4(1, 1, 1, showCurve.Evaluate(timer));
            timer += Time.deltaTime * animationSpeed;
            yield return null;
        }
        Color color = panelImage.color;
        color.a = 0;
        panelImage.color = color;

        SetButtonsActive(false);

        Time.timeScale = 1;
        isPaused = false;
        isPanelVisible = false;
    }

    private void Start()
    {
        Color color = panelImage.color;
        color.a = 0;  
        panelImage.color = color;

        SetButtonsActive(false);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPanelVisible)
            {
                StopAllCoroutines();
                StartCoroutine(ShowPanel());
                PauseGame();
            }

        }
    }

  
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting...");
        SceneManager.LoadScene(exitScene);
    }


    public void ReturnToMainMenu()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(mainMenuScene); 
    }

    public void GoToCollection()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(collectionScene);
    }

    public void ResumeGame()
    {
        StopAllCoroutines();
        StartCoroutine(HidePanel());
        robotController.EnableControl();
    }


    private void SetButtonsActive(bool state)
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(state); 
        }
    }

    public void PauseGame()
    {
        robotController.DisableControl();
    }
}
