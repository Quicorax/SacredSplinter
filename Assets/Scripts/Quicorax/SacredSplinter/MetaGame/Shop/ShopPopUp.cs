using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Services;

namespace Quicorax.SacredSplinter.MetaGame.Shop
{
    public class ShopPopUp : VerticalSelectablePopUp
    {
        public override void SpawnElements()
        {
            foreach (var product in Config.Shop)
            {
                Addressables.LoadAddrssComponentObject<ShopProduct>("ShopProduct", _elementsHolder, productData =>
                    productData.Initialize(product, UpdateUI).ManageTaskException());
            }
        }
    }
}