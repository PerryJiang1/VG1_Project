using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LetterCollection : MonoBehaviour
{
    public GameObject[] letters;

    void Start()
    {
        InitializeLetters();
        UpdateLetters();
    }

    void InitializeLetters()
    {
        //letters[0].GetComponent<Button>().interactable = true;
        for (int j = 0; j < letters.Length; j++)
        {
            letters[j].GetComponent<Button>().interactable = false;
            letters[j].GetComponent<Image>().color = Color.black;
        }
    }

    void UpdateLetters()
    {
        for (int i = 0; i < letters.Length; i++)
        {

            if (GameProgressManager.LetterIsCollected(i + 1))
            {
                letters[i].GetComponent<Button>().interactable = true;
                letters[i].GetComponent<Image>().color = Color.white;

            }
            

        }
    }
}
