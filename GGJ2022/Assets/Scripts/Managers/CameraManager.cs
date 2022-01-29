using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager _instance;
    public static CameraManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CameraManager>();
            }
            return _instance;
        }
    }

    private Camera _mainCamera;
    public Camera MainCamera => _mainCamera;

    public void Init()
    {
        _mainCamera = Camera.main;
    }

    public void SetCameraSize(float size)
    {
        _mainCamera.orthographicSize = size;
    }

}
