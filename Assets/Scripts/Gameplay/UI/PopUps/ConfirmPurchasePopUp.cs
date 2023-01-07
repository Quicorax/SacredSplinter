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

    private ImagesService _imageService;

    public void Initialize(ProductData product, Action onConfirm)
    {
        _imageService = ServiceLocator.GetService<ImagesService>();

        _onConfirm = onConfirm;
        _product = product;

        PrintData();
    }

    private void PrintData()
    {
        _priceAmount.text = (-_product.PriceAmount).ToString();
        _rewardAmount.text = _product.RewardAmount.ToString();

        _priceImage.sprite = _imageService.GetViewImage(_product.Price);
        _rewardImage.sprite = _imageService.GetViewImage(_product.Reward);
    }

    public void OnConfirm()
    {
        _onConfirm?.Invoke();
        CloseSelf();
    }
}
