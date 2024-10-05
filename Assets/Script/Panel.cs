using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    public Image panelImage;
    public AnimationCurve showCurve;
    public float animationSpeed;
    public string mainMenuScene;

    
    public GameObject[] buttons; 
    private bool isPaused = false; 
    private bool isPanelVisible = false; 

    IEnumerator ShowPanel()
    {
        float timer = 0;
        panelImage.gameObject.SetActive(true); 
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
        panelImage.gameObject.SetActive(false); 
        SetButtonsActive(false); 
        Time.timeScale = 1; 
        isPaused = false;
        isPanelVisible = false; 
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPanelVisible)
            {
                StopAllCoroutines();
                StartCoroutine(ShowPanel()); 
            }
            else
            {
                
            }
        }
    }

    
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting...");
    }

    
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(mainMenuScene); 
    }

    
    public void ResumeGame()
    {
        StopAllCoroutines();
        StartCoroutine(HidePanel()); 
    
    }

    
    private void SetButtonsActive(bool state)
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(state); 
        }
    }
}
