using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

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

    [SerializeField] private Transform _cameraPivot;

    public void Init()
    {
        _mainCamera = Camera.main;
    }

    public void SetCameraSize(float size)
    {
        Debug.Log($"SetCameraSize {size}");
        _mainCamera.orthographicSize = size;
    }

    public void ShakeCamera()
    {
        _cameraPivot.DOShakePosition(0.5f);
    }

}
#if UNITY_EDITOR
[CustomEditor(typeof(CameraManager))]
public class CameraManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        /*if (GUILayout.Button("Change Level"))
        {
            GameplayManager.Instance.TryChangeGameState(new GameplayStateData() {
                GameStateId = GameStateId.EnterLevel,
                LevelId = TestSetting.Instance.LevelId
            });
        }*/

        if (GUILayout.Button("Shake Camera"))
        {
            CameraManager camera = (CameraManager)target;
            camera.ShakeCamera();
        }
        base.OnInspectorGUI();
    }
}
#endif