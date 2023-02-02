using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using UnityEngine;
using UnityEngine.UI;

namespace Quicorax.SacredSplinter.MetaGame.UI
{
    public class CreditsPopUp : BasePopUp
    {
        private const string DeveloperURL = "https://quicorax.itch.io/";
        private const string IconsURL = "https://game-icons.net/";
        private const string FontURL = "https://www.juancasco.net/";
        private const string MusicURL = "https://pixabay.com/es/users/gioelefazzeri-16466931/";

        [SerializeField] private Button _developerButton;
        [SerializeField] private Button _iconsButton;
        [SerializeField] private Button _fontButton;
        [SerializeField] private Button _musicButton;

        private void Start()
        {
            _developerButton.onClick.AddListener(OpenDeveloperLink);
            _iconsButton.onClick.AddListener(OpenIconsLink);
            _fontButton.onClick.AddListener(OpenFontLink);
            _musicButton.onClick.AddListener(OpenMusicLink);
        }

        private void OpenDeveloperLink() => Application.OpenURL(DeveloperURL);
        private void OpenIconsLink() => Application.OpenURL(IconsURL);
        private void OpenFontLink() => Application.OpenURL(FontURL);
        private void OpenMusicLink() => Application.OpenURL(MusicURL);
    }
}