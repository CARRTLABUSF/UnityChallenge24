using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [Header("Hand's movement")]
    [SerializeField] float maxXRange;
    [SerializeField] float movementSpeed; 

    private bool movingRight = true;
 
    void Update()
    {
        MoveHandBackNForth();
    }

    private void MoveHandBackNForth() {
        if (transform.position.x > maxXRange) {
            movingRight = false;
        } 

        if (transform.position.x < -maxXRange) {
            movingRight = true;
        }

        Vector3 direction = (movingRight) ? Vector3.right : Vector3.left;
        transform.position += movementSpeed * Time.deltaTime * direction;
    }
}
