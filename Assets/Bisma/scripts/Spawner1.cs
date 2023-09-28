using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner1 : MonoBehaviour
{
    public GameObject[] Stars;
    
    
    void FixedUpdate()
    {
        int randomIndex=Random.Range(0, 4);
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-100,11),5,Random.Range(-100,11));

        Instantiate(Stars[randomIndex],randomSpawnPosition,Quaternion.identity);


    }

    
}
