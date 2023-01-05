using DG.Tweening;
using System;

public class NotEnoughtResources : BasePopUp
{
    public override void BaseInitialize(Action onClosePopUp, Action<BaseData> onElementSelected = null)
    {
        base.BaseInitialize(onClosePopUp, onElementSelected);
        Invoke(nameof(StartFade), 0.5f);
    }

    private void StartFade() => _canvasGroup.DOFade(0, 1f).SetEase(Ease.InQuad).OnComplete(CloseSelf);
}
