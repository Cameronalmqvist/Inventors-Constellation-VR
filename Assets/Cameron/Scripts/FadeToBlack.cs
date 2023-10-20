using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeToBlack : MonoBehaviour
{
    public float fadeDuration = 2.0f;  // Duration of the fade effect in seconds
    private Image image;

    [Header("Objects to Preserve")]
    public GameObject musicObject;   // Drag your music object here
    public GameObject uiObject;      // Drag your main UI object here
    public GameObject xrRigObject;  // Drag your XR rig here
    public GameObject additionalObject1;  // Drag your first additional object here
    public GameObject additionalObject2;  // Drag your second additional object here

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

        // Once the fade in is complete, destroy other objects
        DestroyOtherObjects();
    }

    private void DestroyOtherObjects()
    {
        // Fetch all active root gameObjects
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allObjects)
        {
            // Destroy the object if it's not one of the objects to preserve
            if (go != musicObject && go.transform.root != musicObject.transform &&
                go != uiObject && go.transform.root != uiObject.transform &&
                go != xrRigObject && go.transform.root != xrRigObject.transform &&
                go != additionalObject1 && go.transform.root != additionalObject1.transform &&
                go != additionalObject2 && go.transform.root != additionalObject2.transform)
            {
                Destroy(go);
            }
        }
    }
}
