using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class LavaArea : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            ReloadScene();  
        }
    }


    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();  
        SceneManager.LoadScene(currentScene.name); 
    }
}
