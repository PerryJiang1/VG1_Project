using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIPointerChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{    

    public Texture2D cursorTexture; // Assign the cursor texture in the Inspector
    public Vector2 hotspot = Vector2.zero; // Set the cursor hotspot, if needed

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

    // Change cursor on pointer enter
    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
    }

    // Reset cursor on pointer exit
    public void OnPointerExit(PointerEventData eventData)
    {
        DefaultCursor.Instance.SetDefaultCursor();
    }

    // Reset cursor when the scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset the cursor to the default
        DefaultCursor.Instance.SetDefaultCursor();
    }
}
