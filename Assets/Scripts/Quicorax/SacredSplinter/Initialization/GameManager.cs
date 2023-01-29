using Quicorax.SacredSplinter.Services;
using UnityEngine;

namespace Quicorax.SacredSplinter.Initialization
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        
        public Audio Audio;

        private void Awake() => Singletonize();

        private void Singletonize()
        {
            if (Instance == null)
                Instance = this;

            DontDestroyOnLoad(this);
        }
        
    }
}