using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Quicorax.SacredSplinter.Initialization
{
    public class CurtainTransition : MonoBehaviour
    {
        [SerializeField] private Image _inputBlocker;
        [SerializeField] private Transform _viewBlocker;
        [SerializeField] private Canvas _curtainCanvas;

        [SerializeField] private float _transitionTime = 1;

        public void CurtainOn(Action onComplete)
        {
            _inputBlocker.raycastTarget = true;
            SetObjectActive(true);
            MoveCurtain(Screen.width * 0.5f, () => onComplete?.Invoke());
        }

        private void Start()
        {
            SetInitialSize();
            SetInitialPosition();
            CurtainOff();
        }

        private void MoveCurtain(float finalPosition, Action onTransitionComplete)
        {
            _viewBlocker.DOMoveX(finalPosition, _transitionTime).SetEase(Ease.InQuint)
                .OnComplete(() => onTransitionComplete?.Invoke());
        }

        private void SetInitialSize() => GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, 0);

        private void SetInitialPosition() =>
            _viewBlocker.position = new((float)Screen.width / 2, (float)Screen.height / 2, 0);


        private void CurtainOff()
        {
            _inputBlocker.raycastTarget = false;
            MoveCurtain(Screen.width * 1.5f + 50, () => SetObjectActive(false));
        }

        private void SetObjectActive(bool active) => _curtainCanvas.enabled = active;
    }
}