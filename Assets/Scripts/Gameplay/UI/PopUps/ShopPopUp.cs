
public class ShopPopUp : VerticalSelectablePopUp
{
    internal override void SpawnElements()
    {
        ShopModel shop = ServiceLocator.GetService<ModelsService>().GetModel<ShopModel>();

        foreach (ProductData product in shop.Shop)
            InstanceElement<Product>(View).Initialize(product, UpdateUI);
    }
}
