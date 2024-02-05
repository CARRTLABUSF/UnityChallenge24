using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public float moveSpeed = 5.0f; 
    public float moveDistance = 1.0f; 

    private Vector3 initialPosition;
    private bool moving = true;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        
        MoveHand();
    }

    void MoveHand()
    {
        if (moving)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime*-1);
        }

        
        if (Mathf.Abs(transform.position.z - initialPosition.z) >= moveDistance)
        {
            
            moving = !moving;
        }
    }
}
