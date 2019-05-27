using System.Collections;
using System.Collections.Generic;

namespace LevelManagement.Data
{
    public class SaveData
    {
        public string playerName;
        private readonly string defaultPlayerName = "Player";

        public float masterVolume;
        public float sfxVolume;
        public float soundVolume;

        public string hashValue;

        public SaveData()
        {
            playerName = defaultPlayerName;
            masterVolume = 0f;
            sfxVolume = 0f;
            soundVolume = 0f;
            hashValue = string.Empty;
        }
    }
}
