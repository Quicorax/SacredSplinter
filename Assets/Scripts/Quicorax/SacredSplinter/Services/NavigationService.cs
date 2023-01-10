using UnityEngine;
using UnityEngine.SceneManagement;

namespace Quicorax.SacredSplinter.Services
{
    public class NavigationService : IService
    {
        public void NavigateToScene(string sceneName) =>
            SceneManager.LoadScene(sceneName);

        public void ReloadActualScene() =>
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        public void ExitGame()
        {
#if (UNITY_EDITOR || UNITY_WEBGL)
            Debug.Log("Application Quit!");
            return;
#endif
            Application.Quit();
        }
    }
}