using System;
using UnityEngine;
using UnityEngine.UI;

namespace Quicorax.SacredSplinter.MetaGame.UI
{
    public sealed class GameConfigPopUp : BaseConfigPopUp
    {
        [SerializeField] private Button _exitButton;

        private Action _onReturnToMenu;

        public void Initialize(Action onReturnToMenu)
        {
            _onReturnToMenu = onReturnToMenu;
            
            _exitButton.onClick.AddListener(Exit);
        }

        public void Exit()
        {
            CloseSelf();
            _onReturnToMenu?.Invoke();
        }
    }
}