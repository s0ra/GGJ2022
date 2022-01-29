using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectLevelButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Text _text;
    private int _levelId;
    public void Init(int levelId)
    {
        //Debug.Log($"SelectLevelButton Init {levelId}");
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(OnClick);
        _text.text = levelId.ToString();
        _levelId = levelId;
        _button.interactable = true;
    }

    private void OnClick()
    {
        _button.interactable = false;

        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 worldPosition = CameraManager.Instance.
            MainCamera.
                ScreenToWorldPoint(transform.position);
        UIManager.Instance.LoadingScreen.ScaleDownTo(
        //transform.position, ChangeState);
            worldPosition, ChangeState);
    }

    private void ChangeState()
    {
        GameplayManager.Instance.TryChangeGameState(
        new GameplayStateData()
        {
            GameStateId = GameStateId.EnterLevel,
            LevelId = _levelId
        });
        UIManager.Instance.LevelSelectPanel.TryClosePanel();
    }

    public void SetInteractable(bool value)
    {
        _button.interactable = value;
    }

}
