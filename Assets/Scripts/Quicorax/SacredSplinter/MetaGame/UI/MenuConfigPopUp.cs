using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Services;
using UnityEngine;
using Zenject;

namespace Quicorax.SacredSplinter.MetaGame.UI
{
    public sealed class MenuConfigPopUp : BaseConfigPopUp
    {
        [SerializeField] private PopUpLauncher _credits;
        
        [Inject] private IPopUpSpawnerService _popUps;
        
        public void Initialize()
        {
            _credits.Button.onClick.AddListener(OpenCredits);
        }
        
        private void OpenCredits() => _popUps.SpawnPopUp(_credits);
    }
}