using Quicorax.SacredSplinter.Services.EventBus;
using UnityEngine;

namespace Quicorax.SacredSplinter.MetaGame.UI.PopUps
{
    public abstract class VerticalSelectablePopUp : BasePopUp
    {
        [SerializeField] private SimpleEventBus _onResourcesUpdated;
        [SerializeField] protected Transform _elementsHolder;

        public void Initialize() => SpawnElements();
        protected abstract void SpawnElements();
        protected void UpdateUI() => _onResourcesUpdated.NotifyEvent();
    }
}