using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRuntime : MonoBehaviour
{
    [SerializeField] private List<LevelObjectRuntime> _levelObjectRuntimes;
    [SerializeField] private float _cameraSize;
    public float CameraSize => _cameraSize;
    public void Init()
    {
        foreach (var levelObject in _levelObjectRuntimes)
        {
            levelObject.Init();
        }
    }

    public void UpdateLevel()
    {
        foreach (var levelObject in _levelObjectRuntimes)
        {
            levelObject.UpdateObject();
        }
    }

    public void FixedUpdateLevel()
    {
        foreach (var levelObject in _levelObjectRuntimes)
        {
            levelObject.FixedUpdateObject();
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
