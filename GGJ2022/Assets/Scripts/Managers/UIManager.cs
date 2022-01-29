using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
            }
            return _instance;
        }
    }

    public LevelSelectPanel LevelSelectPanel;
    public LoadingScreen LoadingScreen;
    public Button RetryButton;

    public void InitManager()
    {
        LevelSelectPanel.Init();
        LoadingScreen.Init();
        SetRetryButtonActive(false);
        RetryButton.onClick.AddListener(() => {
            GameplayManager.Instance.TryChangeGameState(new GameplayStateData() {
                GameStateId = GameStateId.Lose,
                // = LevelManager.Instance.LevelId
            });
        });
    }

    public void SetRetryButtonActive(bool isActive)
    {
        RetryButton.gameObject.SetActive(isActive);
    }


}
