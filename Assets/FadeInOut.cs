using System.Collections;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    public float fadeDuration = 2.0f;
    private float alpha = 0.0f;
    private Renderer objectRenderer;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        StartCoroutine(StartFade());
    }

    private IEnumerator StartFade()
    {
        Material material = objectRenderer.material;
        Color materialColor = material.color;

        while (alpha < 1.0f)
        {
            alpha += Time.deltaTime / fadeDuration;
            materialColor.a = alpha;
            material.color = materialColor;
            yield return null;
        }

        // Pause for a moment (e.g., 1 second) between fade in and fade out
        yield return new WaitForSeconds(1.0f);

        while (alpha > 0.0f)
        {
            alpha -= Time.deltaTime / fadeDuration;
            materialColor.a = alpha;
            material.color = materialColor;
            yield return null;
        }
    }
}

