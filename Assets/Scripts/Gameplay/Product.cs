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

    private UserModel _userProgression;

    private Transform _parent;

    public void Initialize(Transform parent, ProductData data, UserModel progression)
    {
        _product = data;
        _userProgression = progression;
        _parent = parent;

        _header.text = _product.ProductHeader;
        _priceAmount.text = _product.Price.Amount.ToString();
        _rewardAmount.text = _product.Reward.Amount.ToString();
        _priceIcon.sprite = _product.Price.Item.Image;
        _rewardIcon.sprite = _product.Reward.Item.Image;
    }

    public void TryBuy()
    {
        Instantiate(_confirmPurchasePopUp, _parent).Initialize(_product, PurchaseConfirmed);
    }

    private void PurchaseConfirmed()
    {
        if (_userProgression.GetAmountOfResource(_product.Price.Item.Name) >= _product.Price.Amount)
        {
            _userProgression.SetAmoutOfItem(_product.Price.Item.Name, -_product.Price.Amount);
            _userProgression.SetAmoutOfItem(_product.Reward.Item.Name, _product.Reward.Amount);
        }
        else
            Instantiate(_notEnoughtResourcesPopUp, _parent).Initialize(null, null);
    }
}
