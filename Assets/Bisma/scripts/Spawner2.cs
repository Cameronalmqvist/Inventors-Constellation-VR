using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner2 : MonoBehaviour
{
    public Transform[] points;
    public GameObject[] stars;
    public GameObject[] Star;
    public float interval = 5;
    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
    
       
        {
             Star[1] = Instantiate(stars[1], points[1].transform, transform) as GameObject;
        timer -= interval;

        }
      

    }
        
    
}
