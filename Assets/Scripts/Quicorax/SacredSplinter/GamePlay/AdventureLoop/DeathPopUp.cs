using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quicorax.SacredSplinter.GamePlay.AdventureLoop
{
    public class DeathPopUp : BasePopUp
    {
        [SerializeField] private Button _returnButton;
        [SerializeField] private TMP_Text _killer, _floorReached, _blueCrystalAmount, _goldCoinAmount;

        private AdventureProgressionService _adventureProgression;
        
        public void Initialize(string deathReason)
        {
            _adventureProgression = ServiceLocator.GetService<AdventureProgressionService>();
                                   
            _returnButton.onClick.AddListener(Return);
            
            _killer.text = deathReason;
            _floorReached.text = _adventureProgression.GetCurrentFloor().ToString();
            _blueCrystalAmount.text = _adventureProgression.GetBlueCrystalsBalance().ToString();
            _goldCoinAmount.text = _adventureProgression.GetGoldCoinsBalance().ToString();
        }
        
        private void Return()
        {
            base.CloseSelf();
            ServiceLocator.GetService<NavigationService>().NavigateToMenu();
        }
    }
}
