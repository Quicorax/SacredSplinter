using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using UnityEngine;

namespace Quicorax.SacredSplinter.MetaGame.UI
{
    public class CreditsPopUp : BasePopUp
    {
        private const string DeveloperURL = "https://quicorax.itch.io/";
        private const string IconsURL = "https://game-icons.net/";
        private const string FontURL = "https://www.juancasco.net/";

        public void OpenDeveloperLink() => Application.OpenURL(DeveloperURL);
        public void OpenIconsLink() => Application.OpenURL(IconsURL);
        public void OpenFontLink() => Application.OpenURL(FontURL);
    }
}