using System;
using System.Threading.Tasks;
using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Models;
using Quicorax.SacredSplinter.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quicorax.SacredSplinter.MetaGame.Shop
{
    public class ConfirmPurchasePopUp : BasePopUp
    {
        [SerializeField] private TMP_Text _priceAmount, _rewardAmount;
        [SerializeField] private Image _priceImage, _rewardImage;

        private Action _onConfirm;
        private ProductData _product;

        private AddressablesService _addressables;

        public void Initialize(ProductData product, Action onConfirm)
        {
            _onConfirm = onConfirm;
            _product = product;

            PrintData().ManageTaskException();
        }

        private async Task PrintData()
        {
            _priceAmount.text = (-_product.PriceAmount).ToString();
            _rewardAmount.text = _product.RewardAmount.ToString();

            _priceImage.sprite = await _addressables.LoadAddrssAsset<Sprite>(_product.Price);
            _rewardImage.sprite = await _addressables.LoadAddrssAsset<Sprite>(_product.Reward);
        }

        public void OnConfirm()
        {
            _onConfirm?.Invoke();
            CloseSelf();
        }
    }
}