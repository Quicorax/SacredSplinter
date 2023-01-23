using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Quicorax.SacredSplinter.GamePlay.Interactions.Combat
{
    public class LogMessage : MonoBehaviour
    {
        [SerializeField] private TMP_Text _logText;

        [SerializeField] private int _logTime = 2;
        [SerializeField] private int _logDistance = 200;

        private Tween _alphaTween;
        private Tween _transformTween;

        public void InitializeData(string message)
        {
            _logText.text = message;

            _alphaTween = _logText.DOFade(1, 0.5f)
                .OnComplete(() => _logText.DOFade(0, _logTime - 0.6f).SetEase(Ease.InCubic));

            _transformTween = _logText.transform.DOMoveY(_logDistance, _logTime).SetRelative().SetEase(Ease.OutSine);
        }

        public void Dispose()
        {
            _alphaTween.Kill();
            _transformTween.Kill();
        }
    }
}