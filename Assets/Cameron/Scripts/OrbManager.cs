using System.Collections;
using UnityEngine;

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

        // After all orbs have been activated, wait for 20 seconds before showing the outro UI
        yield return new WaitForSeconds(20f);
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
        fadeToBlackScript.StartFadeIn();
    }
}
