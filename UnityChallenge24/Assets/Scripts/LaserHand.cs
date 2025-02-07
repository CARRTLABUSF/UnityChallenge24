using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHand : MonoBehaviour
{
    //Object hit with a raycast
    private RaycastHit hitObject;
    //Layermask for hit detection
    public LayerMask hitLayer;
    //Ray length
    public float rayLength = 100.0f;

    void Update(){
        Debug.DrawRay(transform.position, -transform.right * rayLength, Color.red);
    }

    //FixedUpdate is used for operations with physics
    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, -transform.right, out hitObject, rayLength, hitLayer)){
            IHittable hittableObject = hitObject.transform.gameObject.GetComponent<IHittable>();
            hittableObject.OnHit(); 
        }
    }
}
