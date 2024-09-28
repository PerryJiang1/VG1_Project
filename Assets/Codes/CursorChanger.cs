using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    public Texture2D cursorTexture; // Your custom cursor texture

    public Vector2 hotSpot = Vector2.zero; // Adjust to set the cursor's hotspot
    private CursorMode cursorMode = CursorMode.Auto;

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
}
