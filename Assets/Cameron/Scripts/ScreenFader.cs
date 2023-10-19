using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    public float fadeDuration = 1.0f;
    private Image fadeImage;

    private void Awake()
    {
        fadeImage = GetComponentInChildren<Image>();
    }

    public void FadeOut()
    {
        StartCoroutine(FadeTo(1.0f));  // fade to black
    }

    public void FadeIn()
    {
        StartCoroutine(FadeTo(0.0f));  // fade to transparent
    }

    private IEnumerator FadeTo(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float elapsed = 0;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeDuration);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, newAlpha);
            yield return null;
        }

        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, targetAlpha);
    }
}

