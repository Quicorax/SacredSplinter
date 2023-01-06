using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BasePopUp : MonoBehaviour
{
    [SerializeField]
    internal CanvasGroup _canvasGroup;

    private Action<Button> _onClosePopUp;
    private PopUpLauncher _selfPopUp;

    public virtual void Initialize(PopUpLauncher popUpBundle, Action<Button> onClosePopUp)
    {
        _selfPopUp = popUpBundle;
        _onClosePopUp = onClosePopUp;

        _canvasGroup.DOFade(0, 0.2f).From();
    }
    public virtual void CloseSelf()
    {
        _canvasGroup.DOFade(0, 0.2f).OnComplete(() => Destroy(gameObject));

        _onClosePopUp?.Invoke(_selfPopUp.Button);
    }
}
