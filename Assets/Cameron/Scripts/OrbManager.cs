using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OrbManager : MonoBehaviour
{
    public LerpFloatingOrb[] orbsInOrder; // Drag and drop orbs in the order you want them to lerp
    public float delayBetweenOrbs = 5f;   // Time gap between each orb lerping
    public GameObject outroUI;            // Drag and drop your UI GameObject here
    private FadeToBlack fadeToBlackScript; // Reference to the FadeToBlack script

    private int currentOrbIndex = 0;

    void Start()
    {
        fadeToBlackScript = outroUI.GetComponent<FadeToBlack>(); // Get the FadeToBlack script attached to the outroUI
        StartCoroutine(DelayedStart());
    }

    IEnumerator ControlOrbs()
    {
        while (currentOrbIndex < orbsInOrder.Length)
        {
            orbsInOrder[currentOrbIndex].StartLerping();
            currentOrbIndex++;
            yield return new WaitForSeconds(delayBetweenOrbs);
        }

        // After all orbs have been activated, show the outro UI
        ShowOutroUI();
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(6f);
        StartCoroutine(ControlOrbs());
    }

    void ShowOutroUI()
    {
        outroUI.SetActive(true);

        // After the outro UI is shown, start the fade-in effect
        FadeToBlack fadeToBlackScript = outroUI.GetComponent<FadeToBlack>();
        if (fadeToBlackScript != null)
        {
            fadeToBlackScript.StartFadeIn();
        }
    }

}
