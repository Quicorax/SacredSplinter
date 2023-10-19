using Quicorax.SacredSplinter.Initialization;
using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Quicorax.SacredSplinter.MetaGame.UI
{
    public class BaseConfigPopUp : BasePopUp
    {
        [SerializeField] private Button _soundButton;

        [SerializeField] private GameObject _audioON;

        [Inject] private IGameProgressionService _progression;

        private void Start()
        {
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