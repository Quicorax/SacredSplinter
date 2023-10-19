using UnityEngine;
using Zenject;

namespace Quicorax.SacredSplinter.Services
{
    public class Audio : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioClip _mainMusic;
        
        [Inject] private IGameProgressionService _gameProgression;

        private bool _initialized;
        
        public void Initialize()
        {
            if (_initialized)
            {
                return;
            }

            _initialized = true;
            
            _source.clip = _mainMusic;
            _source.Play();

            var audioOff = _gameProgression.GetSoundOff();
            ToggleMuteAudio(!audioOff);
        }

        public void ToggleMuteAudio(bool mute) => _source.mute = mute;
    }
}