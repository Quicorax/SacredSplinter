using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Services;
using TMPro;
using UnityEngine;

namespace Quicorax.SacredSplinter.MetaGame.UI
{
    public class MenuConfigPopUp : BaseConfigPopUp
    {
        [SerializeField] private TMP_Text _languageDisplay;

        [SerializeField] private PopUpLauncher _credits;

        private SaveLoadService _saveLoad;

        private void Start()
        {
            _saveLoad = ServiceLocator.GetService<SaveLoadService>();

            SetSound(ServiceLocator.GetService<GameProgressionService>());
        }

        public void OpenCredits() => ServiceLocator.GetService<PopUpSpawnerService>().SpawnPopUp(_credits);

        public void Save() => _saveLoad.Save();

        public void ChangeLanguage(bool next)
        {
            Debug.Log("LANGUAGE");
        }

        public void ResetApp()
        {
            _saveLoad.DeleteLocalFiles();
            ServiceLocator.GetService<NavigationService>().ExitGame();
        }
    }
}