using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Services;
using UnityEngine;

namespace Quicorax.SacredSplinter.MetaGame.UI
{
    public class BaseConfigPopUp : BasePopUp
    {
        [SerializeField] private GameObject _audioON;

        private GameProgressionService _progression;

        internal void SetSound(GameProgressionService progression)
        {
            _progression = progression;

            TurnAudio(_progression.CheckSoundOff());
        }

        public void ToggleAudio()
        {
            var isOn = !_progression.CheckSoundOff();

            _progression.SetSoundOff(isOn);

            TurnAudio(isOn);
        }

        private void TurnAudio(bool isOn) => _audioON.SetActive(isOn);
    }
}