using DG.Tweening;
using System;
using UnityEngine;

public class BasePopUp : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _canvasGroup;

    private Action _onClosePopUp;

    public void Initialize(Action onClosePopUp)
    {
        _onClosePopUp = onClosePopUp;
        _canvasGroup.DOFade(0, 0.2f).From();
    }
    public void CloseSelf()
    {
        _canvasGroup.DOFade(0, 0.2f).OnComplete(() =>
        {
            Destroy(gameObject);
        });

        _onClosePopUp.Invoke();
    }
}
