using System.Threading.Tasks;
using Quicorax.SacredSplinter.Services;
using Quicorax.SacredSplinter.Services.EventBus;
using UnityEngine;
using Zenject;

namespace Quicorax.SacredSplinter.GamePlay.Interactions
{
    public abstract class AdventureRoomPopUp : BaseRoomPopUp
    {
        [SerializeField] protected SimpleEventBus OnResourcesUpdated;

        [Inject] protected IAdventureConfigurationService AdventureConfig;
        [Inject] protected IGameProgressionService GameProgression;
        [Inject] protected IAddressablesService Addressables;
        [Inject] protected IAdventureProgressionService AdventureProgression;
        [Inject] protected IPopUpSpawnerService PopUpSpawner;

        protected void ExecuteCommonMethods()
        {
            SetSpritesAsync().ManageTaskException();
            SetButtonLogic();
        }

        protected override void Complete()
        {
            GameProgression.SetRoomCompleted();
            base.Complete();
        }

        protected abstract Task SetSpritesAsync();
        protected abstract void SetButtonLogic();
    }
}