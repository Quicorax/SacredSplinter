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
        [SerializeField] private TMP_Text _progressionText;
        [SerializeField] private CurtainTransition _curtain;

        [SerializeField] private Image _progressionBar;
        [SerializeField] private Image _appNotDeadDebug;

        [SerializeField] private int _totalServicesCount = 5;

        private Tween _appNotDeadTween, _progressionTween;
        private ServiceFeeder _serviceFeeder;

        private int _servicesLoadedCount;
        private bool _faded;

        private void Awake()
        {
            _serviceFeeder = new ServiceFeeder();

            Initialize().ManageTaskException();

            _progressionBar.fillAmount = 0;

            AnimateNotDeathAppImage();
        }

        private async Task Initialize()
        {
            await _serviceFeeder.LoadServices(_servicesElements, UpdateProgression);

            _curtain.CurtainOn(() =>
            {
                _appNotDeadTween.Kill();
                _progressionTween.Kill();
                ServiceLocator.GetService<NavigationService>().NavigateToMenu();
            });
        }

        private void UpdateProgression(string message)
        {
            _progressionText.text = message;

            var amount = (float)_servicesLoadedCount++ / _totalServicesCount;
            _progressionTween = _progressionBar.DOFillAmount(amount, 1);
        }

        private void AnimateNotDeathAppImage()
        {
            _faded = !_faded;

            _appNotDeadTween = _appNotDeadDebug.DOFade(_faded ? 1 : 0, 1)
                .OnComplete(AnimateNotDeathAppImage);
        }
    }
}