using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Services;

namespace Quicorax.SacredSplinter.MetaGame.Shop
{
    public class ShopPopUp : VerticalSelectablePopUp
    {
        private IPopUpSpawnerService _popUpSpawner;

        public void SetDependencies(IPopUpSpawnerService popUpSpawner)
        {
            _popUpSpawner = popUpSpawner;
        }

        public override void SpawnElements(IAddressablesService addressables, IGameConfigService config, IGameProgressionService progression)
        {
            base.SpawnElements(addressables, config, progression);

            foreach (var product in Config.Shop)
            {
                Addressables.LoadAddrssComponentObject<ShopProduct>("ShopProduct", _elementsHolder, productData =>
                    productData.Initialize(progression, _popUpSpawner, addressables, product, UpdateUI).ManageTaskException());
            }
        }
    }
}