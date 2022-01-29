using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestSetting : MonoBehaviour
{
    private static TestSetting _instance;
    public static TestSetting Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TestSetting>();
            }
            return _instance;
        }
    }
    public int LevelId;
}

[CustomEditor(typeof(TestSetting))]
public class TestSettingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Change Level"))
        {
            GameplayManager.Instance.TryChangeGameState(new GameplayStateData() {
                GameStateId = GameStateId.EnterLevel,
                LevelId = TestSetting.Instance.LevelId
            });
        }
        base.OnInspectorGUI();
    }
}
