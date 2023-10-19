using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UItimer : MonoBehaviour
{
    public GameObject objectToActivate; // Reference to the GameObject to activate
    public float time = 10f;

    void Start()
    {
        Invoke("Hide", time);
    }

    void Hide()
    {
        Destroy(gameObject);

        // Activate the other GameObject
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }
}

