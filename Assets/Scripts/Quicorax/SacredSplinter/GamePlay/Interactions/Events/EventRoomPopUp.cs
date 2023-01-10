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

        private EventsModel _model;
        private EventData _currentEvent;
        private GameProgressionService _gameProgression;
        private AdventureProgressionService _adventureProgression;

        public void Start()
        {
            _model = ServiceLocator.GetService<ModelsService>().GetModel<EventsModel>("Events");
            _gameProgression = ServiceLocator.GetService<GameProgressionService>();
            _adventureProgression = ServiceLocator.GetService<AdventureProgressionService>();

            _currentEvent = SetEvent();

            _header.text = _currentEvent.Header;
            _action.text = _currentEvent.Action;

            _image.sprite = ServiceLocator.GetService<ElementImages>().GetViewImage(_currentEvent.Concept);
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

            if (success)
            {
                OnSuccess();
                Invoke(nameof(Complete), 1f);
            }
            else
            {
                OnFail();
                Invoke(nameof(Complete), 1f);
            }
            
        }

        private void OnSuccess()
        {
            Debug.Log(_currentEvent.SuccedHeader);
            
            switch (_currentEvent.SuccedKind)
            {
                default:
                    _gameProgression.SetAmountOfResource(_currentEvent.SuccedKind,
                        Random.Range(_currentEvent.SuccedMinAmount, _currentEvent.SuccedMaxAmount));
                    _onResourcesUpdated.NotifyEvent();
                    break;
                case "Health":
                    _adventureProgression.UpdateHealth(_currentEvent.SuccedMaxAmount);
                    break;
            }
        }

        private void OnFail()
        {
            Debug.Log(_currentEvent.FailHeader);

            switch (_currentEvent.SuccedKind)
            {
                default:
                    _gameProgression.SetAmountOfResource(_currentEvent.FailKind, _currentEvent.FailAmount);
                    _onResourcesUpdated.NotifyEvent();
                    break;
                case "Health":
                    _adventureProgression.UpdateHealth(-_currentEvent.FailAmount);
                    break;
            }
        }

        public void OnIgnore() => Complete();
    }
}