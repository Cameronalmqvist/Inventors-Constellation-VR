using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed;
    void Start()
    {
        
    }

    
    void Update()
    {
       
        Vector3 targetDirection = Vector3.Normalize(Vector3.zero - transform.position);

        transform.position += targetDirection * speed * Time.deltaTime;
    }
}
