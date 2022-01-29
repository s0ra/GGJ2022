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

    public void FitOrthoSizeToCamera(Camera mainCamera)
    {
        float orthoSize = mainCamera.orthographicSize;
        //float targetSize = orthoSize * Screen.width * 1f / Screen.height;
        float targetSize = orthoSize * mainCamera.aspect;
        //Debug.Log($"FitOrthoSizeToCamera mainCamera:{orthoSize} * {Screen.width}/{Screen.height} =targetSize:{targetSize}");
        Debug.Log($"FitOrthoSizeToCamera mainCamera:{orthoSize} * {mainCamera.aspect} =targetSize:{targetSize}");
        SetOrthoSize(targetSize);
    }

    private void SetOrthoSize(float size)
    {
        _camera.orthographicSize = size;
    }
}
