using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Services;
using UnityEngine;

namespace Quicorax.SacredSplinter.MetaGame.UI
{
    public sealed class MenuConfigPopUp : BaseConfigPopUp
    {
        [SerializeField] private PopUpLauncher _credits;

        public void Initialize()
        {
            _credits.Button.onClick.AddListener(OpenCredits);
        }
        
        private void OpenCredits() => ServiceLocator.GetService<PopUpSpawnerService>().SpawnPopUp(_credits);
    }
}