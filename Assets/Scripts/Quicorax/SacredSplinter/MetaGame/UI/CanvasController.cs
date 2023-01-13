using DG.Tweening;
using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Services;
using Quicorax.SacredSplinter.Services.EventBus;
using TMPro;
using UnityEngine;

namespace Quicorax.SacredSplinter.MetaGame.UI
{
    public class CanvasController : MonoBehaviour
    {
        [SerializeField] private SimpleEventBus _onResourcesUpdated;

        [SerializeField] private PopUpLauncher _config, _resources;

        [SerializeField] private TMP_Text _coinsAmount, _crystalsAmount;

        private PopUpSpawnerService _popUpSpawner;
        private GameProgressionService _gameProgression;

        private bool _onTween;

        private void Start()
        {
            _popUpSpawner = ServiceLocator.GetService<PopUpSpawnerService>();
            _gameProgression = ServiceLocator.GetService<GameProgressionService>();

            SetItemAmount();
        }

        private void Awake()
        {
            _onResourcesUpdated.Event += SetItemAmount;
        }

        private void OnDestroy()
        {
            _onResourcesUpdated.Event -= SetItemAmount;
        }

        public void OpenConfiguration() => _popUpSpawner.SpawnPopUp(_config);
        public void OpenResources() => _popUpSpawner.SpawnPopUp(_resources);

        private void SetItemAmount()
        {
            _coinsAmount.text = _gameProgression.GetAmountOfResource("Gold Coin").ToString();
            _crystalsAmount.text = _gameProgression.GetAmountOfResource("Blue Crystal").ToString();

            if (!_onTween)
            {
                _onTween = true;

                _crystalsAmount.transform.DOPunchScale(Vector3.one * 0.2f, 0.5f);
                _coinsAmount.transform.DOPunchScale(Vector3.one * 0.2f, 0.5f)
                    .OnComplete(() => _onTween = false);
            }
        }
    }
}