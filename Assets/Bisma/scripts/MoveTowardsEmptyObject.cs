using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsEmptyObject : MonoBehaviour
{
    [SerializeField] private GameObject target; 
    [SerializeField]public float speed = 5f; 

    void Update()
    {
        
        transform.position = Vector3.MoveTowards(transform.position,target.transform.position,speed*Time.deltaTime);

        transform.up = target.transform.position - transform.position;

    } 

}

