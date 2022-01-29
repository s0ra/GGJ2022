using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrthoCamera : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        Camera mainCamera = Camera.main;

        FitOrthoSizeToCamera(mainCamera);
    }

    public void FitOrthoSizeToCamera(Camera mainCamera)
    {
        float orthoSize = mainCamera.orthographicSize;
        float targetSize = orthoSize * Screen.width * 1f / Screen.height;
        SetOrthoSize(targetSize);
    }

    private void SetOrthoSize(float size)
    {
        _camera.orthographicSize = size;
    }
}
