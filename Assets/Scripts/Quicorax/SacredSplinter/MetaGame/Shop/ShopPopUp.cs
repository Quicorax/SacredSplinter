using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Models;
using Quicorax.SacredSplinter.Services;

namespace Quicorax.SacredSplinter.MetaGame.Shop
{
    public class ShopPopUp : VerticalSelectablePopUp
    {
        protected override void SpawnElements()
        {
            foreach (var product in ServiceLocator.GetService<GameConfigService>().Shop)
                InstanceElement<Product>(View).Initialize(product, UpdateUI);
        }
    }
}