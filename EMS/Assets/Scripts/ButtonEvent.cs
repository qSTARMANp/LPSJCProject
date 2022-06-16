using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvent : MonoBehaviour
{
    float m_Timer;
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public CanvasGroup BackgroundImageCanvasGroup;
    // Start is called before the first frame update


    public void OnClickStart()
    {
        Debug.Log("버튼클릭");
        StartCoroutine(FadeIn(BackgroundImageCanvasGroup, 5.0f));
    }

    void StartLevel(CanvasGroup imageCanvasGroup)
    {
        m_Timer += Time.deltaTime;

        imageCanvasGroup.alpha = fadeDuration  / m_Timer;
        if (m_Timer > fadeDuration + displayImageDuration)
        {
            //start = false;
        }
    }

    public IEnumerator FadeIn(CanvasGroup imageCanvasGroup, float time)
    {
        while (imageCanvasGroup.alpha > 0f)
        {
            imageCanvasGroup.alpha = Time.deltaTime / time;
            yield return null;
        }
    }

    public IEnumerator FadeOut(CanvasGroup imageCanvasGroup, float time)
    {
        while (imageCanvasGroup.alpha < 1f)
        {
            imageCanvasGroup.alpha = time / Time.deltaTime ;
            yield return null;
            Debug.Log("페이드 아웃");
        }
    }
}
