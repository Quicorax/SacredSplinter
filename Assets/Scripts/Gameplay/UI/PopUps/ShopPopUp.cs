using UnityEngine;

public class ShopPopUp : VerticalSelectablePopUp
{

    [SerializeField]
    private ProductModel _productModel;

    [SerializeField]
    private Transform _productHolder;

    internal override void SpawnElements()
    {
        foreach (ProductData product in _productModel.Products)
            InstanceElement<Product>(View).Initialize(product, UpdateUI);
    }
}
