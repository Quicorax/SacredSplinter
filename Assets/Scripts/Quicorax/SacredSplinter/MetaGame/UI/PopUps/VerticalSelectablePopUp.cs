using Quicorax.SacredSplinter.Services.EventBus;
using UnityEngine;

namespace Quicorax.SacredSplinter.MetaGame.UI.PopUps
{
    public abstract class VerticalSelectablePopUp : BasePopUp
    {
        [SerializeField] private SimpleEventBus _onResourcesUpdated;

        public GameObject View;

        [SerializeField] private Transform _elementsHolder;

        public void Initialize() => SpawnElements();

        protected virtual void SpawnElements() { }

        protected T InstanceElement<T>(GameObject view) => Instantiate(view, _elementsHolder).GetComponent<T>();
        protected void UpdateUI() => _onResourcesUpdated.NotifyEvent();
    }
}