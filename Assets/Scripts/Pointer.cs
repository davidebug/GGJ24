using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public Texture2D cursorTexture;
    //public Texture2D cursorTexture2;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public bool rotateCursor = false;
    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // Set flag to enable rotation when mouse button is pressed
            rotateCursor = true;
        }
        else
        {
            // Reset rotation when mouse button is released
            rotateCursor = false;
        }
    }
}
