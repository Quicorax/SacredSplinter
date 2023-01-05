using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmPurchasePopUp : BasePopUp
{
    [SerializeField]
    private TMP_Text _priceAmount, _rewardAmount;
    [SerializeField]
    private Image _priceImage, _rewardImage;

    private Action _onConfirm;
    private ProductData _product;
    
    public void Initialize(ProductData product, Action onConfirm)
    {
        _onConfirm = onConfirm;
        _product = product;

        PrintData();
    }

    private void PrintData()
    {
        _priceAmount.text = (-_product.Price.Amount).ToString();
        _rewardAmount.text = _product.Reward.Amount.ToString();

        _priceImage.sprite = _product.Price.Item.Image;
        _rewardImage.sprite = _product.Reward.Item.Image;
    }

    public void OnConfirm()
    {
        _onConfirm?.Invoke();
        CloseSelf();
    }
}
