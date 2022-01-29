using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    public static InputManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<InputManager>();
            }

            return _instance;
        }
    }

    public Texture2D CursurTexture;

    public void InitManager()
    {
        Cursor.SetCursor(CursurTexture, Vector2.zero, CursorMode.ForceSoftware);
    }
}