using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorChanger : MonoBehaviour
{
    public Texture2D cursorTexture; // Your custom cursor texture

    public Vector2 hotSpot = Vector2.zero; // Adjust to set the cursor's hotspot
    private CursorMode cursorMode = CursorMode.Auto;

    private void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    // Called when the mouse enters the object
    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    // Called when the mouse exits the object
    private void OnMouseExit()
    {
        DefaultCursor.Instance.SetDefaultCursor();
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset the cursor to the default
        DefaultCursor.Instance.SetDefaultCursor();
    }
}
