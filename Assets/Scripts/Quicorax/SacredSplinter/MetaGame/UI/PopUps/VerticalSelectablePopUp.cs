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

        protected IAddressablesService Addressables;
        protected IGameConfigService Config;

        public virtual void SpawnElements(IAddressablesService addressables, IGameConfigService config, IGameProgressionService progression)
        {
            Addressables = addressables;
            Config = config;
        }

        protected void UpdateUI() => _onResourcesUpdated.NotifyEvent();
    }
}