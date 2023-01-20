using Quicorax.SacredSplinter.Initialization;
using TMPro;
using UnityEngine;

namespace Quicorax.SacredSplinter.GamePlay.AdventureLoop
{
    public sealed class DeathPopUp : GameOverPopUp
    {
        [SerializeField] private TMP_Text _killer, _floorReached, _blueCrystalAmount, _goldCoinAmount;
       
        public void Initialize(string deathReason, CurtainTransition curtain)
        {
            SetData(curtain);
            
            _killer.text = deathReason;
            _floorReached.text = AdventureProgression.GetCurrentFloor().ToString();
            _blueCrystalAmount.text = AdventureProgression.GetBlueCrystalsBalance().ToString();
            _goldCoinAmount.text = AdventureProgression.GetGoldCoinsBalance().ToString();
        }
    }
}