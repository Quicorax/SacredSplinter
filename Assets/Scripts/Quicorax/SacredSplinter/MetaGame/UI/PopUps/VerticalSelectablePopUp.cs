using Quicorax.SacredSplinter.Services;
using Quicorax.SacredSplinter.Services.EventBus;
using UnityEngine;

namespace Quicorax.SacredSplinter.MetaGame.UI.PopUps
{
    public abstract class VerticalSelectablePopUp : BasePopUp
    {
        [SerializeField] private SimpleEventBus _onResourcesUpdated;
        [SerializeField] protected Transform _elementsHolder;

        protected GameProgressionService Progression;
        protected AddressablesService Addressables;
        protected GameConfigService Config;

        public void Initialize()
        {
            Addressables = ServiceLocator.GetService<AddressablesService>();
            Progression = ServiceLocator.GetService<GameProgressionService>();
            Config = ServiceLocator.GetService<GameConfigService>();
            SpawnElements();
        }

        protected abstract void SpawnElements();
        protected void UpdateUI() => _onResourcesUpdated.NotifyEvent();
    }
}