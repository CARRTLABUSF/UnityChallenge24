using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    //Current hand position
    private Vector3 currentPosition;
    //Left movement boundary
    private float ZLeftBoundary;
    //Right movement boundary
    private float ZRightBoundary;
    //Boundary margin constant
    private const float boundaryMargin = 0.05f;
    //Movement direction modifier
    private int moveDirection = -1;
    //Movement speed
    public float moveSpeed = 1.5f;
    //Movement distance in one direction
    public float moveRadius = 1.5f;

    void Start()
    {
        currentPosition = transform.position;
        ZLeftBoundary = currentPosition.z - moveRadius;
        ZRightBoundary = currentPosition.z + moveRadius;
    }

    //Update is called once per frame
    void Update()
    {
        if (currentPosition.z <= ZLeftBoundary || currentPosition.z >= ZRightBoundary){
            //Change movement direction
            moveDirection *= -1;
            //Move hand by a certain margin to avoid getting stuck out of bounds
            currentPosition.z += moveDirection * boundaryMargin;
        }
        currentPosition.z += moveDirection * moveSpeed * Time.deltaTime;
        transform.position = currentPosition;
    }
}
