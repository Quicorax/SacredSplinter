using System;
using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Models;
using Quicorax.SacredSplinter.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quicorax.SacredSplinter.MetaGame.Shop
{
    public class Product : MonoBehaviour
    {
        [SerializeField] private PopUpLauncher _confirmPurchasePopUp, _notEnoughtResourcesPopUp;

        [SerializeField] private TMP_Text _header, _priceAmount, _rewardAmount;
        [SerializeField] private Image _priceIcon, _rewardIcon;

        private ProductData _product;

        private GameProgressionService _progression;
        private PopUpSpawnerService _popUpSpawner;
        private ElementImagesService _elementImage;

        private Action _onTransactionCompleted;

        public void Initialize(ProductData data, Action onTransactionCompleted)
        {
            _progression = ServiceLocator.GetService<GameProgressionService>();
            _popUpSpawner = ServiceLocator.GetService<PopUpSpawnerService>();
            _elementImage = ServiceLocator.GetService<ElementImagesService>();

            _product = data;
            _onTransactionCompleted = onTransactionCompleted;

            _header.text = _product.Header;

            _priceAmount.text = _product.PriceAmount.ToString();
            _rewardAmount.text = _product.RewardAmount.ToString();

            _priceIcon.sprite = _elementImage.GetViewImage(_product.Price);
            _rewardIcon.sprite = _elementImage.GetViewImage(_product.Reward);
        }

        public void TryBuy()
        {
            _popUpSpawner.SpawnPopUp<ConfirmPurchasePopUp>(_confirmPurchasePopUp)
                .Initialize(_product, PurchaseConfirmed);
        }

        private void PurchaseConfirmed()
        {
            if (_progression.GetAmountOfResource(_product.Price) >= _product.PriceAmount)
            {
                _progression.SetAmountOfResource(_product.Price, -_product.PriceAmount);
                _progression.SetAmountOfResource(_product.Reward, _product.RewardAmount);

                _onTransactionCompleted?.Invoke();
            }
            else
                _popUpSpawner.SpawnPopUp<NotEnoughResources>(_notEnoughtResourcesPopUp).Initialize();
        }
    }
}