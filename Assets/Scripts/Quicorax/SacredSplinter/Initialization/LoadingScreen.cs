using System.Threading.Tasks;
using DG.Tweening;
using Quicorax.SacredSplinter.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Quicorax.SacredSplinter.Initialization
{
    public class LoadingScreen : MonoBehaviour
    {
        //const string _enviroment = "development";
        const string _enviroment = "production";
        
        [SerializeField] private ServiceElements _servicesElements;
        [SerializeField] private TMP_Text _progressionText;
        [SerializeField] private CurtainTransition _curtain;

        [SerializeField] private Image _progressionBar;
        [SerializeField] private Image _appNotDeadDebug;

        [SerializeField] private int _totalServicesCount = 5;
        
        [Inject] private IServicesInitializer _servicesInitializer;
        [Inject] private ILoginGameService _loginGameService;
        [Inject] private IRemoteConfigService _remoteConfigService;
        [Inject] private IGameConfigService _gameConfigService;
        [Inject] private ISaveLoadService _saveLoadService;
        [Inject] private IPopUpSpawnerService _popUpSpawnerService;
        [Inject] private IAdventureProgressionService _adventureProgressionService;
        [Inject] private IGameProgressionProvider _gameProgressionProvider;
        [Inject] private IAddressablesService _addressablesService;
        [Inject] private INavigationService _navigation;
        
        private Tween _appNotDeadTween, _progressionTween;

        private int _servicesLoadedCount;
        private bool _faded;

        private void Awake()
        {
            Initialize().ManageTaskException();

            _progressionBar.fillAmount = 0;

            AnimateNotDeathAppImage();
        }

        private async Task Initialize()
        {
            await InitializeServices();

            _curtain.CurtainOn(() =>
            {
                _appNotDeadTween.Kill();
                _progressionTween.Kill();
                _navigation.NavigateToMenu();
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

        private async Task InitializeServices()
        {
            UpdateProgression("Sharpening Swords");
            await _servicesInitializer.Initialize(_enviroment);
            UpdateProgression("Setting Artifacts");
            await _loginGameService.Initialize();
            UpdateProgression("Invoking Monsters");
            await _remoteConfigService.Initialize();
            UpdateProgression("Defining Relics");
            await _gameProgressionProvider.Initialize();
            UpdateProgression("Calling Warriors");
            await _addressablesService.Initialize(_servicesElements.AssetsToPrewarm.Assets);
            UpdateProgression("Casting Spells");
            
            _gameConfigService.Initialize();
            _saveLoadService.Initialize();
            
            _popUpSpawnerService.Initialize(_servicesElements.PopUpTransformParent);
            _adventureProgressionService.Initialize(_servicesElements.OnPlayerDeath);
        }
    }
}