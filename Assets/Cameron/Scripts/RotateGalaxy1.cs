using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGalaxy1 : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        transform.Rotate(Vector3.down * speed * Time.deltaTime);
    }
}
