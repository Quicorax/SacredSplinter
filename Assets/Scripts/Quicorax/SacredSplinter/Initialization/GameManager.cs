using UnityEngine;

namespace Quicorax.SacredSplinter.Initialization
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        private void Awake() => Singletonize();

        private void Singletonize()
        {
            if (Instance == null)
                Instance = this;

            DontDestroyOnLoad(this);
        }
    }
}