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
    private TMP_Text _header, _priceAmount, _rewardAmount;
    [SerializeField]
    private Image _priceIcon, _rewardIcon;

    private ProductData _product;

    public void Initialize(ProductData data)
    {
        _product = data;

        _header.text = data.ProductHeader;
        _priceAmount.text = data.Price.Amount.ToString();
        _rewardAmount.text = data.Reward.Amount.ToString();
        _priceIcon.sprite = data.Price.Icon;
        _rewardIcon.sprite = data.Reward.Icon;
    }

    public void TryBuy()
    {
        Debug.Log("BUY: " + _product.ProductHeader);
    }
}
