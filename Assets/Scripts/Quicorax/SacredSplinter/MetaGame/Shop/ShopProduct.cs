using System;
using System.Threading.Tasks;
using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Models;
using Quicorax.SacredSplinter.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Quicorax.SacredSplinter.MetaGame.Shop
{
    public class ShopProduct : MonoBehaviour
    {
        [SerializeField] private PopUpLauncher _confirmPurchasePopUp, _notEnoughtResourcesPopUp;

        [SerializeField] private TMP_Text _header, _priceAmount, _rewardAmount;
        [SerializeField] private Image _priceIcon, _rewardIcon;

        private IGameProgressionService _progression;
        private IPopUpSpawnerService _popUpSpawner;

        private ProductData _product;
        private Action _onTransactionCompleted;

        public async Task Initialize(IGameProgressionService progression, IPopUpSpawnerService popUpSpawner,
            IAddressablesService addressables, ProductData data, Action onTransactionCompleted)
        {
            _progression = progression;
            _popUpSpawner = popUpSpawner;
            _product = data;
            
            _onTransactionCompleted = onTransactionCompleted;

            _header.text = _product.Header;

            _priceAmount.text = _product.PriceAmount.ToString();
            _rewardAmount.text = _product.RewardAmount.ToString();

            _priceIcon.sprite = await addressables.LoadAddrssAsset<Sprite>(_product.Price);
            _rewardIcon.sprite = await addressables.LoadAddrssAsset<Sprite>(_product.Reward);
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