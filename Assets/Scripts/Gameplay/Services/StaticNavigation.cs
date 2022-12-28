using UnityEngine;
using UnityEngine.SceneManagement;

namespace Quicorax
{
    public static class StaticNavigation
    {
        public static void NavigateToScene(string sceneName) =>
            SceneManager.LoadScene(sceneName);
        
        public static void ReloadActualScene() =>
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        public static void ExitGame()
        {
#if (UNITY_EDITOR || UNITY_WEBGL)
            Debug.Log("Application Quit!");
            return;
#endif

            Application.Quit();
        }
    }
}