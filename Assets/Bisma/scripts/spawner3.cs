using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UIElements;

public class spawner3 : MonoBehaviour
{
    public Transform[] points;
    public GameObject[] stars;
    private int currentIndex = 0;

    private void Start()
    {
        InvokeRepeating("SpawnStar", 0.01f, 0.01f);
    }

    void SpawnStar()
    {
        if (currentIndex < points.Length)
        {
            if (currentIndex < stars.Length)
            {
                GameObject newStar = Instantiate(stars[currentIndex], points[currentIndex].position, points[currentIndex].rotation, transform);
                currentIndex++;
            }
            
        }
        
    }
}







