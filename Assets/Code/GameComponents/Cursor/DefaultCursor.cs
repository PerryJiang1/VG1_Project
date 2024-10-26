using UnityEngine;

public class DefaultCursor : MonoBehaviour
{
    public static DefaultCursor Instance { get; private set; }

    public Texture2D defaultCursorTexture; // Default cursor texture
    public Vector2 defaultHotSpot = Vector2.zero; // Default cursor hotspot

    private void Awake()
    {
        // Ensure only one instance of the CursorManager exists
        if (Instance == null)
        {   
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep the cursor manager across scenes
            Cursor.SetCursor(defaultCursorTexture, defaultHotSpot, CursorMode.Auto);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to set the cursor to default
    public void SetDefaultCursor()
    {
        Cursor.SetCursor(defaultCursorTexture, defaultHotSpot, CursorMode.Auto);
    }
}
