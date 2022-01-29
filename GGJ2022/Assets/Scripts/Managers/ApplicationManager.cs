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

        // UIManager.Instance.LevelSelectPanel.TryShowPanel();
    }

    public void StartCoroutine(Action onComplete)
    {
        StartCoroutine(StartCorou(onComplete));
    }

    private IEnumerator StartCorou(Action onComplete)
    {
        //yield return new WaitForSeconds(seconds);
        yield return new WaitForEndOfFrame();
        onComplete?.Invoke();
        yield return null;
    }

}
