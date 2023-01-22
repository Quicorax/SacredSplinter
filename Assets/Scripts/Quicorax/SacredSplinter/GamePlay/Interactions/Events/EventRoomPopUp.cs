using System.Threading.Tasks;
using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Models;
using Quicorax.SacredSplinter.Services;
using Quicorax.SacredSplinter.Services.EventBus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quicorax.SacredSplinter.GamePlay.Interactions.Events
{
    public sealed class EventRoomPopUp : AdventureRoomPopUp
    {
        [SerializeField] private SimpleEventBus _onResourcesUpdated;
        [SerializeField] private TMP_Text _header, _action;
        [SerializeField] private Image _image;
        [SerializeField] private PopUpLauncher _eventResultPopUp;
        [SerializeField] private Button _ignoreButton;
        [SerializeField] private Button _actionButton;

        private EventData _currentEvent;

        
        protected override void Initialize()
        {
            GetCommonServices();
            
            
            _currentEvent = SetEvent();
            ExecuteCommonMethods();
            
            _header.text = _currentEvent.Header;
            _action.text = _currentEvent.Action;
        }

        protected override async Task SetSpritesAsync() =>
            _image.sprite =await Addressables.LoadAddrssAsset<Sprite>(_currentEvent.Concept);
        
        protected override void SetButtonLogic()
        {
            _ignoreButton.interactable = AdventureConfig.GetHeroData().CanIgnoreEvents;

            if (_ignoreButton.interactable)
                _ignoreButton.onClick.AddListener(Complete);
            
            _actionButton.onClick.AddListener(OnInteract);
        }

        private EventData SetEvent()
        {
            EventData data = null;
            var dataSelected = false;
            
            var dataList = ServiceLocator.GetService<GameConfigService>().Events;

            while (!dataSelected)
            {
                data = dataList[Random.Range(0, dataList.Count)];
                if (data.Active && (string.IsNullOrEmpty(data.Location) || data.Location.Equals(AdventureConfig.GetLocation().Header)))
                {
                    dataSelected = true;
                }
            }

            return data;
        }
        
        private void OnInteract() => OnInteractAsync().ManageTaskException();
        private async Task OnInteractAsync()
        {
            var success = Random.Range(0, 100) <= _currentEvent.Chance;
            var header = success ? _currentEvent.SuccedHeader : _currentEvent.FailHeader;
            var kind = success ? _currentEvent.SuccedKind : _currentEvent.FailKind;
            var amount = success
                ? Random.Range(_currentEvent.SuccedMinAmount, _currentEvent.SuccedMaxAmount)
                : _currentEvent.FailAmount;

            OnEventResult(kind, amount, _currentEvent.DeathMotive);

            Sprite image = null;
            var withImage = false;
            
            if (kind != "Health")
            {
                withImage = true;   
                image = await Addressables.LoadAddrssAsset<Sprite>(kind);
            }

            PopUpSpawner.SpawnPopUp<EventResultPopUp>(_eventResultPopUp)
                .SetData(header, amount.ToString(), image, withImage);

            Complete();
        }

        private void OnEventResult(string kind, int amount, string reason)
        {
            switch (kind)
            {
                default:
                    GameProgression.SetAmountOfResource(kind, amount);
                    _onResourcesUpdated.NotifyEvent();
                    break;
                case "Health":
                    AdventureProgression.UpdateProportionalHealth(amount, reason);
                    break;
            }
        }
    }
}