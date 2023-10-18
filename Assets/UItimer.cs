using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UItimer : MonoBehaviour
{
    float time = 15f;
 
    void Start()
    {
        Invoke("Hide",time);
    }

    // Update is called once per frame
    void Hide()
    {
        Destroy(gameObject);
    }
}
