using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LevelManagement.Data;

namespace LevelManagement
{
    public class SettingsMenu : Menu<SettingsMenu>
    {
        [SerializeField]
        private Slider _masterVolumeSlider;

        [SerializeField]
        private Slider _sfxVolumeSlider;

        [SerializeField]
        private Slider _soundVolumeSlider;

        private DataManager _dataManager;
        
        protected override void Awake()
        {
            base.Awake();

            _dataManager = FindObjectOfType<DataManager>();
        }

        void Start()
        {
           LoadData();
        }

        public void OnMasterVolumeChanged(float volume)
        {
             if(_dataManager != null)
             {
                 _dataManager.MasterVolume = volume;
             }
        }

        public void OnSFXVolumeChanged(float volume)
        {
            if(_dataManager != null)
             {
                 _dataManager.SFXVolume = volume;
             }
        }

        public void OnSoundVolumeChanged(float volume)
        {
            if(_dataManager != null)
             {
                 _dataManager.SoundVolume = volume;
             }
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            if(_dataManager != null)
            {
                _dataManager.Save();
            }
        }

        public void LoadData()
        {
            if(_dataManager == null || _masterVolumeSlider == null || _sfxVolumeSlider == null || _soundVolumeSlider == null)
            {
                return;
            }

            _dataManager.Load();

            _masterVolumeSlider.value = _dataManager.MasterVolume;
            _sfxVolumeSlider.value = _dataManager.SFXVolume;
            _soundVolumeSlider.value = _dataManager.SoundVolume;
        }
    }
}
