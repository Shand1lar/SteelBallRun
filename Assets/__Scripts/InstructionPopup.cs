using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionPopup : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeInTime = 1f;
    public float visibleTime = 3f;
    public float fadeOutTime = 1f;

    void Start()
    {
        StartCoroutine(FadeRoutine());
    }

    IEnumerator FadeRoutine()
    {
        // Fade In
        float t = 0;
        while (t < fadeInTime)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = t / fadeInTime;
            yield return null;
        }

        // Stay visible
        yield return new WaitForSeconds(visibleTime);

        // Fade Out
        t = 0;
        while (t < fadeOutTime)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = 1 - (t / fadeOutTime);
            yield return null;
        }

        canvasGroup.alpha = 0;
    }
}
