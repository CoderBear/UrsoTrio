using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement
{
    [RequireComponent(typeof(ScreenFadeLoader))]
    public class SplashScreen : MonoBehaviour
    {
        [SerializeField]
        private ScreenFadeLoader _screenFader;

        [SerializeField]
        private float delay = 1f;

        void Awake()
        {
            _screenFader = GetComponent<ScreenFadeLoader>();
        }

        void Start()
        {
            _screenFader.FadeOn();
        }

        public void FadeAndLoad()
        {
            StartCoroutine(FadeAndLoadRoutine());
        }

        IEnumerator FadeAndLoadRoutine()
        {
            yield return new WaitForSeconds(delay);
            _screenFader.FadeOff();
            LevelLoader.LoadMainMenuLevel();

            yield return new WaitForSeconds(_screenFader.FadeOnDuration);

            Destroy(gameObject);
        }
    }
}
