using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelManagement
{
    public class MainMenu : Menu<MainMenu>
    {
        public void OnPlayPressed()
        {
            // load primary game level buildIndex = 1
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            GameMenu.Open();
        }

        public void OnSettingsPressed()
        {
            // SettingsMenu.Open();
        }

        public void OnCreditsPressed()
        {
            // CreditsScreen.Open();
        }

        public override void OnBackPressed()
        {
        }
    }
}
