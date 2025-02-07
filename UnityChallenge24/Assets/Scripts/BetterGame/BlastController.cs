using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastController : MonoBehaviour
{
    //Current position vector
    private Vector3 currentPosition;
    //Movement speed
    public float moveSpeed = 100.0f;

    void Start()
    {
        currentPosition = transform.position;
    }

    void Update()
    {
        currentPosition.z += moveSpeed * Time.deltaTime;
        transform.position = currentPosition;
    }
}
