using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeToBlack : MonoBehaviour
{
    public float fadeDuration = 2.0f;  // Duration of the fade effect in seconds
    private Image image;

    [Header("Objects to Destroy")]
    public GameObject objectToDestroy1;  // Drag the first object you want to destroy here
    public GameObject objectToDestroy2;  // Drag the second object you want to destroy here  // Drag the fifth object you want to destroy here

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = image.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            image.color = color;
            yield return null;
        }

        // Ensure the image is fully opaque
        color.a = 1f;
        image.color = color;

        // Once the fade in is complete, destroy specified objects
        DestroySpecifiedObjects();
    }

    private void DestroySpecifiedObjects()
    {
        DestroyObjectAndChildren(objectToDestroy1);
        DestroyObjectAndChildren(objectToDestroy2);
    }

    private void DestroyObjectAndChildren(GameObject obj)
    {
        if (obj != null)
        {
            Destroy(obj);
        }
    }
}
