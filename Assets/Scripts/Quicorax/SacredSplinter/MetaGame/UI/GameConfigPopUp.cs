using System;
using Quicorax.SacredSplinter.Services;

namespace Quicorax.SacredSplinter.MetaGame.UI
{
    public class GameConfigPopUp : BaseConfigPopUp
    {
        private Action _onReturnToMenu;
        public void Initialize(Action onReturnToMenu) => _onReturnToMenu = onReturnToMenu;
        public void Exit()
        {
            CloseSelf();
            _onReturnToMenu?.Invoke();
        }
        
        private void Start() => SetSound(ServiceLocator.GetService<GameProgressionService>());
    }
}