using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEditor;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGrouop;
    [SerializeField] private RectTransform _holeParent;
    [SerializeField] private Text LevelIdText;

    private int maxSize = 2500;
    public void Init()
    {
        gameObject.SetActive(false);
        LevelIdText.gameObject.SetActive(false);

    }

    public void ScaleDownToScreen(Vector2 screenPosition, Action onComplete)
    {
        _holeParent.transform.position = screenPosition;
        _holeParent.sizeDelta = 4000 * Vector2.one;
        gameObject.SetActive(true);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(_holeParent.DOSizeDelta(200* Vector2.one, 0.5f));
        sequence.AppendInterval(0.5f);
        sequence.Append(_holeParent.DOSizeDelta(Vector2.zero, 0.5f));
        sequence.OnComplete(() => {
            onComplete?.Invoke();
        });
    }

    public void ScaleDownTo(Vector2 worldPosition, Action onComplete)
    {
        Vector2 screenPosition = CameraManager.Instance.
            MainCamera.
                WorldToScreenPoint(worldPosition);
        //Debug.Log($"ScaleDownTo worldPosition{worldPosition} screenPosition{screenPosition}");
        _holeParent.transform.position = screenPosition;
        _holeParent.sizeDelta = 4000 * Vector2.one;
        gameObject.SetActive(true);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(_holeParent.DOSizeDelta(200 * Vector2.one, 0.5f));
        sequence.AppendInterval(0.5f);
        sequence.Append(_holeParent.DOSizeDelta(Vector2.zero, 0.5f));
        sequence.OnComplete(()=> {
            onComplete?.Invoke();
          });
    }

    public void ScaleDownToAnchor(Vector2 anchorPosition, Action onComplete)
    {
        _holeParent.anchoredPosition = anchorPosition;
        _holeParent.sizeDelta = 4000 * Vector2.one;
        gameObject.SetActive(true);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(_holeParent.DOSizeDelta(200 * Vector2.one, 0.5f));
        sequence.AppendInterval(0.5f);
        sequence.Append(_holeParent.DOSizeDelta(Vector2.zero, 0.5f));
        sequence.OnComplete(() => {
            onComplete?.Invoke();
        });
    }

    public void ScaleUpFrom(Vector2 worldPosition,int levelId, Action onComplete)
    {
        Vector2 screenPosition = CameraManager.Instance.
            MainCamera.
                WorldToScreenPoint(worldPosition);
        //Debug.Log($"ScaleDownTo worldPosition{worldPosition} screenPosition{screenPosition}");
        _holeParent.transform.position = screenPosition;

        _holeParent.sizeDelta = Vector2.zero;
        gameObject.SetActive(true);
        LevelIdText.color = new Color(1,1,1,0);
        LevelIdText.gameObject.SetActive(true);
        LevelIdText.text = levelId.ToString();
        Sequence sequence = DOTween.Sequence();
        sequence.Append(LevelIdText.DOFade(1, 0.5f));
        sequence.AppendInterval(0.5f);
        sequence.Append(LevelIdText.DOFade(0,0.5f));
        sequence.Join(_holeParent.DOSizeDelta(
            4000 * Vector2.one, 0.5f).SetEase(Ease.InCirc));

        sequence.OnComplete(() => {
            onComplete?.Invoke();
            gameObject.SetActive(false);
            LevelIdText.gameObject.SetActive(false);

        });
    }

}

[CustomEditor(typeof(LoadingScreen))]
public class LoadingScreenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("ScaleUp"))
        {
            LoadingScreen screen = (LoadingScreen)target;
            screen.ScaleUpFrom(Vector3.zero,0, null);
        }

        if (GUILayout.Button("ScaleDown"))
        {
            LoadingScreen screen = (LoadingScreen)target;
            screen.ScaleDownTo(Vector3.zero, null);
        }
        base.OnInspectorGUI();
    }
}
