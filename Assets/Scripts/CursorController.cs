using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Texture2D cursorImage;

    public void ChangeCursorImage()
    {
        Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.Auto);
    }

    public void DefaultCursorImage()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
