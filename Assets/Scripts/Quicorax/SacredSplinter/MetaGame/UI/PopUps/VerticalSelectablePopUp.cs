using Quicorax.SacredSplinter.Services;
using Quicorax.SacredSplinter.Services.EventBus;
using UnityEngine;
using Zenject;

namespace Quicorax.SacredSplinter.MetaGame.UI.PopUps
{
    public abstract class VerticalSelectablePopUp : BasePopUp
    {
        [SerializeField] private SimpleEventBus _onResourcesUpdated;
        [SerializeField] protected Transform _elementsHolder;

        [Inject] protected IAddressablesService Addressables;
        [Inject] protected IGameConfigService Config;

        public abstract void SpawnElements();
        protected void UpdateUI() => _onResourcesUpdated.NotifyEvent();
    }
}