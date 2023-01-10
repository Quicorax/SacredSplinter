using Quicorax.SacredSplinter.Models;
using Quicorax.SacredSplinter.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quicorax.SacredSplinter.GamePlay.Interactions.Events
{
    public class EventRoomPopUp : BaseRoomPopUp
    {
        [SerializeField] private TMP_Text _header, _action;
        [SerializeField] private Image _image;

        private EventsModel _model;
        private EventData _event;

        public void Start()
        {
            _model = ServiceLocator.GetService<ModelsService>().GetModel<EventsModel>("Events");
            _event = _model.Events[Random.Range(0, _model.Events.Count)];

            _header.text = _event.Header;
            _action.text = _event.Action;

            _image.sprite = ServiceLocator.GetService<ElementImages>().GetViewImage(_event.Concept);
        }

        public void OnInteract()
        {
            var success = Random.Range(0, 100) <= _event.Chance;

            if (success)
            {
                Debug.Log(_event.SuccedHeader);
                Invoke(nameof(Complete), 1f);
            }
            else
            {
                Debug.Log(_event.FailHeader);
                Invoke(nameof(Complete), 1f);
            }
        }

        public void OnIgnore() => Complete();
    }
}