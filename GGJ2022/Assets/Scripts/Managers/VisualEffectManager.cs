using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VisualEffectManager : MonoBehaviour
{
    private static VisualEffectManager _instance;
    public static VisualEffectManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<VisualEffectManager>();
            }
            return _instance;
        }
    }

    private Dictionary<VisualEffectId, VisualEffectRuntime> _visualEffectPrefabs;

    public void InitManager()
    {
        _visualEffectPrefabs = new Dictionary<VisualEffectId, VisualEffectRuntime>();
        LoadAllVisualEffectPrefabs();
    }

    private void LoadAllVisualEffectPrefabs()
    {
        foreach (VisualEffectId visualEffectId in Enum.GetValues(typeof(VisualEffectId)))
        {
            VisualEffectRuntime visualEffectRuntime = Resources.Load<GameObject>(
                GameConstants.ResourcesPath.VisualEffectPath + visualEffectId)
                    .GetComponent<VisualEffectRuntime>();
            if (visualEffectRuntime == null)
            {
                Debug.LogError($"Cannot find VisualEffectRuntime{visualEffectId} in resources");
                continue;
            }

            _visualEffectPrefabs.Add(visualEffectId, visualEffectRuntime);
            ObjectPoolManager.Instance.CacheObject(visualEffectRuntime.gameObject,1, 
            (go)=> 
            {
                go.GetComponent<VisualEffectRuntime>().Init();
            });
        }
    }

    public VisualEffectRuntime SpawnVisualEffect(VisualEffectSpawnData visualEffectSpawnData)
    {
        VisualEffectId id = visualEffectSpawnData.VisualEffectId;
        if (!_visualEffectPrefabs.ContainsKey(id))
        {
            Debug.LogError($"_visualEffectPrefabs not contains {id}");
            return null;
        }
        VisualEffectRuntime result = ObjectPoolManager.Instance.CreateObject(
            _visualEffectPrefabs[id].gameObject).GetComponent<VisualEffectRuntime>();
        result.OnSpawn(visualEffectSpawnData);
        return result;
    }
}
