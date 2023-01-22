using System.Threading.Tasks;
using Quicorax.SacredSplinter.Services;

namespace Quicorax.SacredSplinter.GamePlay.Interactions
{
    public abstract class AdventureRoomPopUp : BaseRoomPopUp
    {
        protected AdventureConfigurationService AdventureConfig;
        protected GameProgressionService GameProgression;
        protected AddressablesService Addressables;
        protected AdventureProgressionService AdventureProgression;
        protected PopUpSpawnerService PopUpSpawner;


        protected void GetCommonServices()        
        {
            AdventureProgression = ServiceLocator.GetService<AdventureProgressionService>();
            AdventureConfig = ServiceLocator.GetService<AdventureConfigurationService>();
            GameProgression = ServiceLocator.GetService<GameProgressionService>();
            Addressables = ServiceLocator.GetService<AddressablesService>();
            PopUpSpawner = ServiceLocator.GetService<PopUpSpawnerService>();

        }

        protected void ExecuteCommonMethods()
        {
            SetSpritesAsync().ManageTaskException();
            SetButtonLogic();
        }

        protected abstract Task SetSpritesAsync();
        protected abstract void SetButtonLogic();
    }
}