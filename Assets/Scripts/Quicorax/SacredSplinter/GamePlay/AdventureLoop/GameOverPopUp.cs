using Quicorax.SacredSplinter.Initialization;
using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Quicorax.SacredSplinter.GamePlay.AdventureLoop
{
    public abstract class GameOverPopUp : BasePopUp
    {
        [SerializeField] private Button _returnButton;
    
        private CurtainTransition _curtain;
        protected AdventureProgressionService AdventureProgression;

        protected void SetData(CurtainTransition curtain)
        {
            _curtain = curtain;
            AdventureProgression = ServiceLocator.GetService<AdventureProgressionService>();

            _returnButton.onClick.AddListener(Return);
        }
        protected void Return()
        {
            base.CloseSelf();
            _curtain.CurtainON(()=> ServiceLocator.GetService<NavigationService>().NavigateToMenu());
        }
    }
}