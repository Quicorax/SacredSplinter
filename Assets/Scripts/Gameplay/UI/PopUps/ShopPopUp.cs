using Quicorax;
using UnityEngine;

public class ShopPopUp : BasePopUp
{
    [SerializeField]
    private SimpleEventBus _onResourcesUpdated;

    [SerializeField]
    private ProductModel _productModel;

    [SerializeField]
    private Product _productView;

    [SerializeField]
    private Transform _productHolder;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        SpawnProducts();
    }
    private void SpawnProducts()
    {
        foreach (ProductData product in _productModel.Products)
            Instantiate(_productView, _productHolder).Initialize(product, UpdateUI);
    }

    private void UpdateUI()
    {
        _onResourcesUpdated.NotifyEvent();
    }
}
