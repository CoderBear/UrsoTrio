using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelManagement
{
    public class PauseMenu : Menu<PauseMenu>
    {
        [SerializeField]
        private int mainMenuIndex = 0;

        public void OnResumePressed()
        {
            Time.timeScale =1;
            base.OnBackPressed();
        }

        public void OnRestartPressed()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            base.OnBackPressed();
        }

        public void OnReturnToMainPressed()
        {
            Time.timeScale = 1;
            // Return to Main Menu
            LevelLoader.LoadMainMenuLevel();

            MainMenu.Open();
        }

        public void OnSettingsPressed()
        {
            SettingsMenu.Open();
        }
    }
}
