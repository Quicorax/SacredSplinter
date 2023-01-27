using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Services;

namespace Quicorax.SacredSplinter.MetaGame.Shop
{
    public class ShopPopUp : VerticalSelectablePopUp
    {
        protected override void SpawnElements()
        {
            var popUpSpawner = ServiceLocator.GetService<PopUpSpawnerService>();

            foreach (var product in ServiceLocator.GetService<GameConfigService>().Shop)
            {
                Addressables.LoadAddrssComponentObject<ShopProduct>("ShopProduct", _elementsHolder, productData =>
                    productData.Initialize(product, UpdateUI, Progression, popUpSpawner, Addressables).ManageTaskException());
            }
        }
    }
}