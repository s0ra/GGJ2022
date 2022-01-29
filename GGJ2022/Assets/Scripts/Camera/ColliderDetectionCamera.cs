using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDetectionCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    public void Init()
    {
        Camera mainCamera = Camera.main;
        FitOrthoSizeToCamera(mainCamera);
    }

    private void FitOrthoSizeToCamera(Camera mainCamera)
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
