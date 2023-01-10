using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Models;
using Quicorax.SacredSplinter.Services;

namespace Quicorax.SacredSplinter.MetaGame.Shop
{
    public class ShopPopUp : VerticalSelectablePopUp
    {
        protected override void SpawnElements()
        {
            var shop = ServiceLocator.GetService<ModelsService>().GetModel<ShopModel>("Shop");

            foreach (var product in shop.Shop)
                InstanceElement<Product>(View).Initialize(product, UpdateUI);
        }
    }
}