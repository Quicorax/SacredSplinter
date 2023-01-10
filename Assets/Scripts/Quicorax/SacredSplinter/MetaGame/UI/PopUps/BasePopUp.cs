using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Quicorax.SacredSplinter.MetaGame.UI.PopUps
{
    public abstract class BasePopUp : MonoBehaviour
    {
        [SerializeField] internal CanvasGroup _canvasGroup;

        private Action<Button> _onClosePopUp;
        private PopUpLauncher _selfPopUp;

        public void Initialize(PopUpLauncher popUpBundle, Action<Button> onClosePopUp)
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
}