using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement
{
    public class ModeSelectMenu : Menu<ModeSelectMenu>
    {
        [SerializeField]
        private float _playDelay = 0.5f;

        [SerializeField]
        private TransitionFader startTransitionPrefab;

        public void OnStandardModePressed()
        {
            StartCoroutine(OnPlayPressedRoutine());
        }

        IEnumerator OnPlayPressedRoutine()
        {
            TransitionFader.PlayTransition(startTransitionPrefab);
            LevelLoader.LoadNextLevel();
            yield return new WaitForSeconds(_playDelay);
            GameMenu.Open();
        }

        public void OnCollectModePressed()
        {
            StartCoroutine(OnCollectModePressedRoutine());
        }

        IEnumerator OnCollectModePressedRoutine()
        {
            TransitionFader.PlayTransition(startTransitionPrefab);
            LevelLoader.LoadCollectModeLevel();
            yield return new WaitForSeconds(_playDelay);
            GameMenu.Open();
        }

        public void OnTimedModePressed()
        {
            StartCoroutine(OnTimedModePressedRoutine());
        }

        IEnumerator OnTimedModePressedRoutine()
        {
            TransitionFader.PlayTransition(startTransitionPrefab);
            LevelLoader.LoadTimedModeLevel();
            yield return new WaitForSeconds(_playDelay);
            GameMenu.Open();
        }
    }
}
