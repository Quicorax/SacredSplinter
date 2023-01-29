using UnityEngine;

namespace Quicorax.SacredSplinter.Services
{
    public class Audio : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioClip _mainMusic;

        private bool _initialized;
        
        public void Initialize()
        {
            if (_initialized)
                return;

            _initialized = true;
            
            _source.clip = _mainMusic;
            _source.Play();

            bool audioOff = ServiceLocator.GetService<GameProgressionService>().GetSoundOff();
            ToggleMuteAudio(!audioOff);
        }

        public void ToggleMuteAudio(bool mute) => _source.mute = mute;
    }
}