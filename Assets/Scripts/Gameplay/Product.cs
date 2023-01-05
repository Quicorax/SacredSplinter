using Quicorax;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ProductData
{
    public string ProductHeader;
    public Reward Price;
    public Reward Reward;
}
public class Product : MonoBehaviour
{
    [SerializeField]
    private ConfirmPurchasePopUp _confirmPurchasePopUp;
    [SerializeField]
    private NotEnoughtResources _notEnoughtResourcesPopUp;

    [SerializeField]
    private TMP_Text _header, _priceAmount, _rewardAmount;
    [SerializeField]
    private Image _priceIcon, _rewardIcon;

    private ProductData _product;

    private GameProgressionService _progression;
    private PopUpSpawnerService _popUpSpawner;

    private Action _onTransactionCompleted;

    public void Initialize(ProductData data, Action onTransactionCompleted)
    {
        _progression = ServiceLocator.GetService<GameProgressionService>();
        _popUpSpawner = ServiceLocator.GetService<PopUpSpawnerService>();

        _product = data;
        _onTransactionCompleted = onTransactionCompleted;

        _header.text = _product.ProductHeader;
        _priceAmount.text = _product.Price.Amount.ToString();
        _rewardAmount.text = _product.Reward.Amount.ToString();
        _priceIcon.sprite = _product.Price.Item.Image;
        _rewardIcon.sprite = _product.Reward.Item.Image;
    }

    public void TryBuy()
    {
        _popUpSpawner.SpawnPopUp<ConfirmPurchasePopUp>(_confirmPurchasePopUp)
            .Initialize(_product, PurchaseConfirmed);
    }

    private void PurchaseConfirmed()
    {
        if (_progression.CheckAmountOfResource(_product.Price.Item.Name) >= _product.Price.Amount)
        {
            _progression.SetAmountOfResource(_product.Price.Item.Name, -_product.Price.Amount);
            _progression.SetAmountOfResource(_product.Reward.Item.Name, _product.Reward.Amount);

            _onTransactionCompleted?.Invoke();
        }
        else
            _popUpSpawner.SpawnPopUp<NotEnoughtResources>(_notEnoughtResourcesPopUp);
    }
}
