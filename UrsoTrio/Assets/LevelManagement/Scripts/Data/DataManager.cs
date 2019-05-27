using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement.Data
{
    public class DataManager : MonoBehaviour
    {
        private SaveData _saveData;
        private JsonSaver _jsonSaver;

        public float MasterVolume { get { return _saveData.masterVolume; } set { _saveData.masterVolume = value; } }
        public float SFXVolume { get { return _saveData.sfxVolume; } set { _saveData.sfxVolume = value; } }
        public float SoundVolume { get { return _saveData.soundVolume; } set { _saveData.soundVolume = value; } }

        private void Awake()
        {
            _saveData = new SaveData();
            _jsonSaver = new JsonSaver();
        }

        public void Save()
        {
            _jsonSaver.Save(_saveData);
        }

        public void Load()
        {
            _jsonSaver.Load(_saveData);
        }
    }
}
