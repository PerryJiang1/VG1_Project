using UnityEngine;
using UnityEngine.UI; // Include for working with UI
using TMPro; // Import TextMeshPro namespace

public class LetterInteraction : MonoBehaviour
{
    public TextMeshPro letterText; // Reference to TextMeshPro
    public string letterContent; // The content of the letter
    public GameObject paperSprite; // Reference to the paper sprite
    private bool playerNearby = false; // Track player proximity
    public Vector3 offset; // Offset to adjust text position relative to paper
    public GameObject letter; // Reference to the letter GameObject
    public int levelNumber;



    private void Start()
    {
        HideLetterContent();
        AlignLetter();
    }



      void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            ShowLetterContent();
            Debug.Log("1234");
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            HideLetterContent();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            GameProgressManager.CollectLetter(levelNumber);

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            HideLetterContent();
            if (letter != null)
            {
                letter.SetActive(false); 
            }
        }
    }

    void ShowLetterContent()
    {
        paperSprite.SetActive(true); // Show the paper sprite
        if (letterText != null){
            letterText.text = letterContent; // Set the content dynamically
            letterText.enabled = true; // Ensure the text is visible
        }
 
    }

    void HideLetterContent()
    {   
        if (paperSprite != null){
             paperSprite.SetActive(false); // Hide the paper sprite
        if (letterText != null){
        letterText.text = ""; // Clear the content
        letterText.enabled = false; // Hide the text
        }
       
          }
 
    }


    void AlignLetter()
    {
        // Convert the world position of the paper to screen position
    letterText.rectTransform.position = paperSprite.transform.position + offset;

    }
}
