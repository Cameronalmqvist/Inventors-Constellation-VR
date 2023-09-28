using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    public GameObject[] stars;
    public Transform[] points;
    public float beat = (60 / 130);
    private float timer;

 

    void Update()
    {

        if (timer > beat)
        {

            GameObject star= Instantiate(stars[Random.Range(0, 18)], points[Random.Range(0,8)]);
            star.transform.localPosition = Vector3.zero;
            star.transform.Rotate(transform.forward, 90 * Random.Range(0, 4));
            timer -= beat;

        }

        timer += Time.deltaTime;

    }

}
