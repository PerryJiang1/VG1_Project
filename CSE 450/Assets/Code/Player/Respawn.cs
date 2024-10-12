using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    public float fallThreshold = -10f;

    void Update()
    {
        if (transform.position.y < fallThreshold)
        {
            ResetScene();
        }
    }

    void ResetScene()
    {
        // Reset the scene if falling out of map
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
