using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class LevelButtons : MonoBehaviour
{
    public GameObject[] levelButtons;

    void Start()
    {
        InitializeLevelButtons();
        UpdateLevelButtons();
    }

    void InitializeLevelButtons()
    {
        levelButtons[0].GetComponent<Button>().interactable = true;
        for (int j = 1; j < levelButtons.Length; j++)
        {
            levelButtons[j].GetComponent<Button>().interactable = false;
            levelButtons[j].GetComponent<Image>().color = Color.black;
        }
    }

    void UpdateLevelButtons()
    {
        for (int i = 1; i < levelButtons.Length; i++)
        {

            if (GameProgressManager.IsLevelComplete(i))
            {
                levelButtons[i].GetComponent<Button>().interactable = true;
                levelButtons[i].GetComponent<Image>().color = Color.white;

            }
            else
            {
                for (int j = i + 1; j < levelButtons.Length; j++)
                {
                    levelButtons[j].GetComponent<Button>().interactable = false;
                    levelButtons[j].GetComponent<Image>().color = Color.black;
                }
                break;
            }

        }
    }
}
