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
            var addressables = ServiceLocator.GetService<AddressablesService>();

            foreach (var product in ServiceLocator.GetService<GameConfigService>().Shop)
            {
                addressables.LoadAddrssComponentObject<ShopProduct>("ShopProduct", _elementsHolder, productData =>
                    productData.Initialize(product, UpdateUI, progression, popUpSpawner, addressables).ManageTaskException());
            }
        }
    }
}