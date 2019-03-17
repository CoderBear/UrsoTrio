using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MaskableGraphic))]
public class ScreenFader : MonoBehaviour
{
    #region Fields
        public float soildAlpha = 1f;
        public float clearAlpha = 0f;
        public float delay = 0f;
        public float timeToFade = 1f;
        MaskableGraphic m_graphic;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        m_graphic = GetComponent<MaskableGraphic>();
        FadeOff();
    }

    #region Coroutines Call Methods
        public void FadeOn()
        {
            StartCoroutine(FadeRoutine(soildAlpha));
        }

        public void FadeOff()
        {
            StartCoroutine(FadeRoutine(clearAlpha));
        }
    #endregion

    #region Coroutines
        IEnumerator FadeRoutine(float alpha)
        {
            yield return new WaitForSeconds(delay);
            m_graphic.CrossFadeAlpha(alpha, timeToFade, true);
        }
    #endregion
}
