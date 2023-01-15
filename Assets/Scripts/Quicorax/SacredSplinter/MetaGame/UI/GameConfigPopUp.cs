using Quicorax.SacredSplinter.Services;

namespace Quicorax.SacredSplinter.MetaGame.UI
{
    public class GameConfigPopUp : BaseConfigPopUp
    {
        private void Start()
        {
            SetSound(ServiceLocator.GetService<GameProgressionService>());
        }

        public void Exit()
        {
            CloseSelf();
            ServiceLocator.GetService<NavigationService>().NavigateToMenu();
        }
    }
}