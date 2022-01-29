using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelSelectPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private SelectLevelButton[] _buttons;

    public void Init()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            int levelId = i + 1;
            _buttons[i].Init(levelId);
        }
    }

    public void UpdatePanel()
    {

    }

    public void TryShowPanel()
    {
        gameObject.SetActive(true);
    }

    public void TryClosePanel()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            int levelId = i + 1;
            _buttons[i].SetInteractable(false);
        }
        gameObject.SetActive(false);
    }
}
