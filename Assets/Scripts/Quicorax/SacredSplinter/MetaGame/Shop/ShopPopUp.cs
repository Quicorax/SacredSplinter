using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Services;

namespace Quicorax.SacredSplinter.MetaGame.Shop
{
    public class ShopPopUp : VerticalSelectablePopUp
    {
        protected override void SpawnElements()
        {
            var progression = ServiceLocator.GetService<GameProgressionService>();
            var popUpSpawner = ServiceLocator.GetService<PopUpSpawnerService>();
            
            foreach (var product in ServiceLocator.GetService<GameConfigService>().Shop)
                InstanceElement<Product>(View).Initialize(product, UpdateUI, progression, popUpSpawner);
        }
    }
}