using Quicorax.SacredSplinter.Initialization;
using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Services;
using UnityEngine;

namespace Quicorax.SacredSplinter.MetaGame.UI
{
    public class BaseConfigPopUp : BasePopUp
    {
        [SerializeField] private GameObject _audioON;

        private GameProgressionService _progression;

        public void ToggleAudio()
        {
            var isOn = !_progression.GetSoundOff();

            _progression.SetSoundOn(isOn);

            TurnAudio(isOn);
        }

        protected void SetSound(GameProgressionService progression)
        {
            _progression = progression;
            TurnAudio(_progression.GetSoundOff());
        }

        private void TurnAudio(bool isOn)
        {
            _audioON.SetActive(isOn);
            GameManager.Instance.Audio.ToggleMuteAudio(!isOn);
        }
    }
}