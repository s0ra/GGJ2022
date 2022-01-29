﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LevelManager>();
            }
            return _instance;
        }
    }

    private LevelRuntime _currentLevelRuntime;

    public void InitManager()
    {

    }

    public void SpawnLevel(int level)
    {
        DestroyLevel();
        GameObject levelPrefab = Resources.Load<GameObject>(GameConstants.ResourcesPath.LevelsPath + level);
        if (levelPrefab == null)
        {
            Debug.Log($"Cannot find level in {GameConstants.ResourcesPath.LevelsPath + level}");
        }
        _currentLevelRuntime = Instantiate(levelPrefab.gameObject).GetComponent<LevelRuntime>();
        _currentLevelRuntime.Init();
    }

    public void UpdateManager()
    {
        if (_currentLevelRuntime !=null)
        {
            _currentLevelRuntime.UpdateLevel();
        }
    }

    public void FixedUpdateManager()
    {
        if (_currentLevelRuntime != null)
        {
            _currentLevelRuntime.FixedUpdateLevel();
        }
    }

    private void DestroyLevel()
    {
        if (_currentLevelRuntime != null)
        {
            _currentLevelRuntime.DestroySelf();
        }
    }
}
