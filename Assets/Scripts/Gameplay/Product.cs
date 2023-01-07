using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ProductData
{
    public string Header;

    public string Price;
    public int PriceAmount;

    public string Reward;
    public int RewardAmount;
}
public class Product : MonoBehaviour
{
    [SerializeField]
    private PopUpLauncher _confirmPurchasePopUp, _notEnoughtResourcesPopUp;

    [SerializeField]
    private TMP_Text _header, _priceAmount, _rewardAmount;
    [SerializeField]
    private Image _priceIcon, _rewardIcon;

    private ProductData _product;

    private GameProgressionService _progression;
    private PopUpSpawnerService _popUpSpawner;
    private ImagesService _imageService;

    private Action _onTransactionCompleted;

    public void Initialize(ProductData data, Action onTransactionCompleted)
    {
        _progression = ServiceLocator.GetService<GameProgressionService>();
        _popUpSpawner = ServiceLocator.GetService<PopUpSpawnerService>();
        _imageService = ServiceLocator.GetService<ImagesService>();

        _product = data;
        _onTransactionCompleted = onTransactionCompleted;

        _header.text = _product.Header;

        _priceAmount.text = _product.PriceAmount.ToString();
        _rewardAmount.text = _product.RewardAmount.ToString();

        _priceIcon.sprite = _imageService.GetViewImage(_product.Price);
        _rewardIcon.sprite = _imageService.GetViewImage(_product.Reward);
    }

    public void TryBuy()
    {
        _popUpSpawner.SpawnPopUp<ConfirmPurchasePopUp>(_confirmPurchasePopUp)
            .Initialize(_product, PurchaseConfirmed);
    }

    private void PurchaseConfirmed()
    {
        if (_progression.CheckAmountOfResource(_product.Price) >= _product.PriceAmount)
        {
            _progression.SetAmountOfResource(_product.Price, -_product.PriceAmount);
            _progression.SetAmountOfResource(_product.Reward, _product.RewardAmount);

            _onTransactionCompleted?.Invoke();
        }
        else
            _popUpSpawner.SpawnPopUp<NotEnoughtResources>(_notEnoughtResourcesPopUp).Initialize();
    }
}
