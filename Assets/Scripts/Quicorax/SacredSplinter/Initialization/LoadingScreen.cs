using System.Threading.Tasks;
using DG.Tweening;
using Quicorax.SacredSplinter.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quicorax.SacredSplinter.Initialization
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private ServiceElements _servicesElements;
        [SerializeField] private Slider _progressionBar;
        [SerializeField] private TMP_Text _progressionText;
        [SerializeField] private CurtainTransition _curtain;
        [SerializeField] private Transform _appNotDeadDebug;
        [SerializeField] private float _animPunch;

        private Tween _animationTween;
        private ServiceFeeder _serviceFeeder;

        private int _servicesLoadedCount;

        private void Awake()
        {
            _serviceFeeder = new ServiceFeeder();

            Initialize().ManageTaskException();

            _progressionBar.maxValue = 5;
            _progressionBar.value = 0;

            AnimateNotDeathAppImage();
        }

        private async Task Initialize()
        {
            await _serviceFeeder.LoadServices(_servicesElements, UpdateProgression);

            _curtain.CurtainON(() =>
            {
                _animationTween.Kill();
                ServiceLocator.GetService<NavigationService>().NavigateToMenu();
            });
        }

        private void UpdateProgression(string message)
        {
            _progressionText.text = message;
            _progressionBar.value = _servicesLoadedCount++;
        }

        private void AnimateNotDeathAppImage() =>
            _animationTween = _appNotDeadDebug.DOPunchPosition(Vector3.up * _animPunch, 1f, 1, 1)
                .OnComplete(AnimateNotDeathAppImage);
    }
}