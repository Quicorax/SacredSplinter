using DG.Tweening;
using Quicorax.SacredSplinter.Initialization;
using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Services;
using Quicorax.SacredSplinter.Services.EventBus;
using TMPro;
using UnityEngine;
using Zenject;

namespace Quicorax.SacredSplinter.MetaGame.UI
{
    public class CanvasController : MonoBehaviour
    {
        [SerializeField] private SimpleEventBus _onResourcesUpdated;

        [SerializeField] private PopUpLauncher _config, _resources;

        [SerializeField] private TMP_Text _coinsAmount, _crystalsAmount;

        [SerializeField] private CurtainTransition _curtain;

        [Inject] private IPopUpSpawnerService _popUpSpawner;
        [Inject] private IGameProgressionService _gameProgression;
        [Inject] private INavigationService _navigation;

        private bool _onTween;

        private void Start()
        {
            GameManager.Instance.Audio.Initialize();

            SetButtonsListener();
            SetItemAmount();
        }
        
        private void Awake() => _onResourcesUpdated.Event += SetItemAmount;
        private void OnDestroy() => _onResourcesUpdated.Event -= SetItemAmount;
        
        private void SetButtonsListener()
        {
            _config.Button.onClick.AddListener(OpenConfiguration);
            _resources.Button.onClick.AddListener(OpenResources);
        }
        private void OpenConfiguration()
        {
            if (_config.PopUp is GameConfigPopUp)
            {
                _popUpSpawner.SpawnPopUp<GameConfigPopUp>(_config).Initialize(() =>
                    _curtain.CurtainOn(() => _navigation.NavigateToMenu()));
            }
            else
                _popUpSpawner.SpawnPopUp<MenuConfigPopUp>(_config).Initialize();
        }

        private void OpenResources() => _popUpSpawner.SpawnPopUp(_resources);

        private void SetItemAmount()
        {
            _coinsAmount.text = _gameProgression.GetAmountOfResource("Gold Coin").ToString();
            _crystalsAmount.text = _gameProgression.GetAmountOfResource("Blue Crystal").ToString();

            if (_onTween)
                return;

            _onTween = true;

            _crystalsAmount.transform.DOPunchScale(Vector3.one * 0.2f, 0.5f);
            _coinsAmount.transform.DOPunchScale(Vector3.one * 0.2f, 0.5f)
                .OnComplete(() => _onTween = false);
        }
    }
}