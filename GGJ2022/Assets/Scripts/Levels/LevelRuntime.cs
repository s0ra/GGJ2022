using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

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
        //Debug.Log($"LevelRuntime Init");
    }

    public void OnEnterGameplayState(GameStateId gameStateId)
    {
        foreach (var levelObject in _levelObjectRuntimes)
        {
            levelObject.OnEnterGameplayState(gameStateId);
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

    public void SearchAndLinkAllLevelObjects()
    {
        _levelObjectRuntimes = transform
                .GetComponentsInChildren<LevelObjectRuntime>().ToList();
    }
}

[CustomEditor(typeof(LevelRuntime))]
public class LevelRuntimeEditor : Editor
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

        if (GUILayout.Button("Link all LevelObjects"))
        {
            LevelRuntime level = (LevelRuntime)target;
            level.SearchAndLinkAllLevelObjects();
        }
        base.OnInspectorGUI();
    }
}
