using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speed = 2.5f;
      transform.position+=Time.deltaTime*transform.forward*speed;  
    }
}
