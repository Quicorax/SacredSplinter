using DG.Tweening;

public class NotEnoughtResources : BasePopUp
{
    public void Initialize() => Invoke(nameof(StartFade), 0.5f);
    private void StartFade() => _canvasGroup.DOFade(0, 1f).SetEase(Ease.InQuad).OnComplete(CloseSelf);
}
