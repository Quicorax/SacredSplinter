using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using UnityEngine;

namespace Quicorax.SacredSplinter.MetaGame.UI
{
    public class CreditsPopUp : BasePopUp
    {
        private const string DeveloperURL = "https://quicorax.itch.io/";
        private const string IconsURL = "https://game-icons.net/";
        private const string FontURL = "https://www.juancasco.net/";
        private const string MusicURL = "https://pixabay.com/es/users/gioelefazzeri-16466931/";

        public void OpenDeveloperLink() => Application.OpenURL(DeveloperURL);
        public void OpenIconsLink() => Application.OpenURL(IconsURL);
        public void OpenFontLink() => Application.OpenURL(FontURL);
        public void OpenMusicLink() => Application.OpenURL(MusicURL);
    }
}