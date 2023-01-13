using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Models;
using Quicorax.SacredSplinter.Services;
using Quicorax.SacredSplinter.Services.EventBus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quicorax.SacredSplinter.GamePlay.Interactions.Events
{
    public sealed class EventRoomPopUp : BaseRoomPopUp
    {
        [SerializeField] private SimpleEventBus _onResourcesUpdated;
        [SerializeField] private TMP_Text _header, _action;
        [SerializeField] private Image _image;
        [SerializeField] private PopUpLauncher _eventResultPopUp;
        [SerializeField] private Button _ignoreButton;

        private EventData _currentEvent;

        private GameProgressionService _gameProgression;
        private AdventureProgressionService _adventureProgression;
        private AdventureConfigurationService _adventureConfig;
        private PopUpSpawnerService _popUpSpawner;

        public void Start()
        {
            GetServices();

            _currentEvent = SetEvent();

            _header.text = _currentEvent.Header;
            _action.text = _currentEvent.Action;
            _image.sprite = ServiceLocator.GetService<ElementImagesService>().GetViewImage(_currentEvent.Concept);

            SetButtonLogic();
        }
        public void OnInteract()
        {
            var success = Random.Range(0, 100) <= _currentEvent.Chance;

            var header = success ? 
                _currentEvent.SuccedHeader :
                _currentEvent.FailHeader;
            
            var kind = success ? 
                _currentEvent.SuccedKind : 
                _currentEvent.FailKind;
            
            var amount = success ? 
                Random.Range(_currentEvent.SuccedMinAmount, _currentEvent.SuccedMaxAmount) : 
                _currentEvent.FailAmount;

            OnEventResult(kind, amount, _currentEvent.DeathMotive);

            var image = ServiceLocator.GetService<ElementImagesService>().GetViewImage(kind);

            _popUpSpawner.SpawnPopUp<EventResultPopUp>(_eventResultPopUp)
                .SetData(header, amount.ToString(), image, kind == "Health");

            Complete();
        }
        private void SetButtonLogic()
        {
            _ignoreButton.interactable = _adventureConfig.GetHeroData().CanIgnoreEvents;
            
            if(_ignoreButton.interactable)
                _ignoreButton.onClick.AddListener(Complete);
        }
        private void GetServices()
        {
            _gameProgression = ServiceLocator.GetService<GameProgressionService>();
            _adventureProgression = ServiceLocator.GetService<AdventureProgressionService>();
            _adventureConfig = ServiceLocator.GetService<AdventureConfigurationService>();
            _popUpSpawner = ServiceLocator.GetService<PopUpSpawnerService>();
        }

        private EventData SetEvent()
        {
            EventData data = null;
            var dataSelected = false;

            while (!dataSelected)
            {
                data =  ServiceLocator.GetService<ModelsService>().GetModel<EventsModel>("Events").GetRandomEvent();
                if (data.Active && (string.IsNullOrEmpty(data.Location) || data.Location.Equals(_adventureConfig.GetLocation())))
                {
                    dataSelected = true;
                }
            }

            return data;
        }
        private void OnEventResult(string kind, int amount, string reason)
        {
            switch (kind)
            {
                default:
                    _gameProgression.SetAmountOfResource(kind, amount);
                    _onResourcesUpdated.NotifyEvent();
                    break;
                case "Health":
                    _adventureProgression.UpdateProportionalHealth(amount, reason);
                    break;
            }
        }
    }
}