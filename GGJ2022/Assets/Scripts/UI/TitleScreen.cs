using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TitleScreen : MonoBehaviour
{
    [SerializeField] private Text startText1;
    [SerializeField] private Text startText2;
    [SerializeField] private Button button;
    [SerializeField] private CanvasGroup canvasGroup;

    private bool _clicked = false;

    public void Init()
    {
        gameObject.SetActive(true);
        button.onClick.AddListener(OnClick);
        startText1.DOFade(0.25f,1)
            .SetLoops(-1,LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        startText2.DOFade(0.25f,1f)
            .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        _clicked = false;
    }

    public void OnClick()
    {
        if (!_clicked)
        {
            _clicked = true;
            startText1.DOKill();
            startText2.DOKill();
            canvasGroup.DOFade(0, 0.5f).OnComplete(() => {
                gameObject.SetActive(false);
            });
        }

    }
}
