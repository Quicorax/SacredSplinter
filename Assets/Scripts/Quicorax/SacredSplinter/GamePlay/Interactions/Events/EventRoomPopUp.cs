using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Models;
using Quicorax.SacredSplinter.Services;
using Quicorax.SacredSplinter.Services.EventBus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quicorax.SacredSplinter.GamePlay.Interactions.Events
{
    public class EventRoomPopUp : BaseRoomPopUp
    {
        [SerializeField] private SimpleEventBus _onResourcesUpdated;
        [SerializeField] private TMP_Text _header, _action;
        [SerializeField] private Image _image;
        [SerializeField] private PopUpLauncher _eventResultPopUp;

        private EventsModel _model;
        private EventData _currentEvent;

        private GameProgressionService _gameProgression;
        private AdventureProgressionService _adventureProgression;
        private PopUpSpawnerService _popUpSpawner;

        public void Start()
        {
            _gameProgression = ServiceLocator.GetService<GameProgressionService>();
            _adventureProgression = ServiceLocator.GetService<AdventureProgressionService>();
            _popUpSpawner = ServiceLocator.GetService<PopUpSpawnerService>();

            _model = ServiceLocator.GetService<ModelsService>().GetModel<EventsModel>("Events");

            _currentEvent = SetEvent();

            _header.text = _currentEvent.Header;
            _action.text = _currentEvent.Action;
            _image.sprite = ServiceLocator.GetService<ElementImagesService>().GetViewImage(_currentEvent.Concept);
        }

        private EventData SetEvent()
        {
            EventData data = null;
            var dataSelected = false;

            while (!dataSelected)
            {
                data = _model.GetRandomEvent();
                if (data.Active)
                {
                    dataSelected = true;
                }
            }

            return data;
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

            OnEventResult(kind, amount);

            var image = ServiceLocator.GetService<ElementImagesService>().GetViewImage(kind);

            _popUpSpawner.SpawnPopUp<EventResultPopUp>(_eventResultPopUp)
                .SetData(header, amount.ToString(), image);

            Complete();

        }

        private void OnEventResult(string kind, int amount)
        {
            switch (kind)
            {
                default:
                    _gameProgression.SetAmountOfResource(kind, amount);
                    _onResourcesUpdated.NotifyEvent();
                    break;
                case "Health":
                    _adventureProgression.UpdateHealth(amount);
                    break;
            }
        }

        public void OnIgnore() => Complete();
    }
}