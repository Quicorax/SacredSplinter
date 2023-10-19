using Quicorax.SacredSplinter.Initialization;
using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Quicorax.SacredSplinter.GamePlay.AdventureLoop
{
    public abstract class GameOverPopUp : BasePopUp
    {
        [SerializeField] private Button _returnButton;
    
        private CurtainTransition _curtain;
        
       [Inject] protected IAdventureProgressionService AdventureProgression;
       [Inject] private INavigationService _navigation;

        protected void SetData(CurtainTransition curtain)
        {
            _curtain = curtain;
            _returnButton.onClick.AddListener(Return);
        }
        
        private void Return()
        {
            base.CloseSelf();
            _curtain.CurtainOn(()=> _navigation.NavigateToMenu());
        }
    }
}