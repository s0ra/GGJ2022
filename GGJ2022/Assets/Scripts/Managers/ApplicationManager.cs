using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ApplicationManager : MonoBehaviour
{
    private static ApplicationManager _instance;
    public static ApplicationManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ApplicationManager>();
            }
            return _instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ObjectPoolManager.Instance.InitManager();
        AudioManager.Instance.InitManager();
        GameSceneManager.Instance.InitManager();
        LevelManager.Instance.InitManager();
        GameplayManager.Instance.InitManager();
        VisualEffectManager.Instance.InitManager();
        UIManager.Instance.InitManager();
        InputManager.Instance.InitManager();
        PixelColliderManager.Instance.InitManager();
        CameraManager.Instance.Init();
    }

    public void StartCoroutine(float seconds, Action onComplete )
    {
        StartCoroutine(StartCorou(seconds, onComplete));
    }

    private IEnumerator StartCorou(float seconds, Action onComplete)
    {
        yield return new WaitForSeconds(seconds);
        onComplete?.Invoke();
        yield return null;
    }

}
