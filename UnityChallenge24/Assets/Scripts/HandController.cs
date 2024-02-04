using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using JetBrains.Annotations;
using UnityEngine;

public class HandController : MonoBehaviour
{   
    //variable will store how fast hand will move
    public int speed;
   
    void Update()
    {   
        //keeps current x and y and uses sine wave to ossiclate z position
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Sin(Time.time * speed));
        
    }

   
}
