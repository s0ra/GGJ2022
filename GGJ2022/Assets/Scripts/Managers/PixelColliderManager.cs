using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelColliderManager : MonoBehaviour
{
    private static PixelColliderManager _instance;

    public static PixelColliderManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PixelColliderManager>();
            }

            return _instance;
        }
    }

    [SerializeField] private PixelPerfectCollider2D pixelPerfectCollider2D;
    [SerializeField] private ColliderDetectionCamera colliderDetectionCamera;

    public void InitManager()
    {
        colliderDetectionCamera.Init();
    }

    public void RegeneratePixelCollider()
    {
        pixelPerfectCollider2D.Regenerate();
    }

    public void FitOrthoSizeToRendererCamera()
    {
        colliderDetectionCamera.FitOrthoSizeToCamera(CameraManager.Instance.MainCamera);
    }


}