using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    public GameObject[] stars;
    public Transform[] points;
    public float beat = (60 / 120);
    private float timer;

 

    void Update()
    {

        if (timer > beat)
        {

            GameObject star= Instantiate(stars[Random.Range(0, 12)], points[Random.Range(0,12)]);
            star.transform.localPosition = Vector3.zero;
            star.transform.Rotate(transform.forward);
            timer -= beat;

        }

        timer += Time.deltaTime;

    }

}
