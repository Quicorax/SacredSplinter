using DG.Tweening;
using Quicorax.SacredSplinter.MetaGame.UI.PopUps;

namespace Quicorax.SacredSplinter.MetaGame.Shop
{
    public class NotEnoughResources : BasePopUp
    {
        public void Initialize() => Invoke(nameof(StartFade), 0.5f);
        private void StartFade() => _canvasGroup.DOFade(0, 1f).SetEase(Ease.InQuad).OnComplete(CloseSelf);
    }
}