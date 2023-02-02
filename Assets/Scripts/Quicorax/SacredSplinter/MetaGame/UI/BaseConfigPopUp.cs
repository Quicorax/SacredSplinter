using Quicorax.SacredSplinter.Initialization;
using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Quicorax.SacredSplinter.MetaGame.UI
{
    public class BaseConfigPopUp : BasePopUp
    {
        [SerializeField] private Button _soundButton;

        [SerializeField] private GameObject _audioON;

        private GameProgressionService _progression;

        private void Start()
        {
            _progression = ServiceLocator.GetService<GameProgressionService>();
            
            _soundButton.onClick.AddListener(ToggleAudio);
            
            SetSound();
        }

        private void SetSound() => TurnAudio(_progression.GetSoundOff());
        
        private void ToggleAudio()
        {
            var isOn = !_progression.GetSoundOff();

            _progression.SetSoundOn(isOn);
            TurnAudio(isOn);
        }

        private void TurnAudio(bool isOn)
        {
            _audioON.SetActive(isOn);
            GameManager.Instance.Audio.ToggleMuteAudio(!isOn);
        }
    }
}