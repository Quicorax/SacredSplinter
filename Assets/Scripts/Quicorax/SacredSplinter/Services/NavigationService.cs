﻿using UnityEngine;
using UnityEngine.SceneManagement;

#pragma warning disable CS0162

namespace Quicorax.SacredSplinter.Services
{
    public class NavigationService : IService
    {
        public void NavigateToMenu() => SceneManager.LoadScene("01_Menu");
        public void NavigateToGame() => SceneManager.LoadScene("02_Game");

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