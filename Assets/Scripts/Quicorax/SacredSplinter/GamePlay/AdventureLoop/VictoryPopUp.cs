using System.Threading.Tasks;
using Quicorax.SacredSplinter.Initialization;
using Quicorax.SacredSplinter.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quicorax.SacredSplinter.GamePlay.AdventureLoop
{
    public sealed class VictoryPopUp : GameOverPopUp
    {
        [SerializeField] private Image _splinterImage, _locationImage;
        [SerializeField] private TMP_Text _location, _blueCrystalAmount, _goldCoinAmount;
        
        public void Initialize(CurtainTransition curtain)
        {
            SetData(curtain);
                                   
            var locationName = ServiceLocator.GetService<AdventureConfigurationService>().GetLocation().Header;
            
            _location.text = locationName;
            SetImage(locationName).ManageTaskException();
            
            _blueCrystalAmount.text = AdventureProgression.GetBlueCrystalsBalance().ToString();
            _goldCoinAmount.text = AdventureProgression.GetGoldCoinsBalance().ToString();

            if (!ServiceLocator.GetService<GameProgressionService>().GetLocationCompleted(locationName))
            {
                AdventureProgression.SetLocationCompleted();
            }
            else
                _splinterImage.color = Color.white * 0.5f;
        }

        private async Task SetImage(string locationName)
        {
            _locationImage.sprite =
                await ServiceLocator.GetService<AddressablesService>().LoadAddrssAsset<Sprite>(locationName);
        }
    }
}
