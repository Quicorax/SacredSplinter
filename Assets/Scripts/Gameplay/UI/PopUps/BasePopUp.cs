using DG.Tweening;
using System;
using UnityEngine;

public class BasePopUp : MonoBehaviour
{
    [SerializeField]
    internal CanvasGroup _canvasGroup;

    private Action _onClosePopUp;

    public virtual void Initialize(Action onClosePopUp, Action<BaseData> onElementSelected = null)
    {
        _onClosePopUp = onClosePopUp;
        _canvasGroup.DOFade(0, 0.2f).From();
    }
    public virtual void CloseSelf()
    {
        _canvasGroup.DOFade(0, 0.2f).OnComplete(() =>
        {
            Destroy(gameObject);
        });

        _onClosePopUp?.Invoke();
    }
}
