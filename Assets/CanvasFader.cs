using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasFader : MonoBehaviour
{
    public CanvasGroup canvasGroup; // Reference to the CanvasGroup component.
    public float fadeDuration = 1.0f; // Duration of the fade-in/out effect.

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(0, 1));
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(1, 0));
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }
}

