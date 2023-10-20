using System.Collections;
using UnityEngine;

public class OrbManager : MonoBehaviour
{
    public LerpFloatingOrb[] orbsInOrder; // Drag and drop orbs in the order you want them to lerp
    public float delayBetweenOrbs = 5f;   // Time gap between each orb lerping

    private int currentOrbIndex = 0;

    void Start()
    {
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
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(6f);
        StartCoroutine(ControlOrbs());
    }
}
