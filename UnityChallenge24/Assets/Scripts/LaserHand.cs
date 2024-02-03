using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class LaserHand : MonoBehaviour
{
    
    float range = 3;
    bool firstCheck = false;
    
    // Update is called once per frame
    void Update()
    {
        checkCollision();
    }

    void checkCollision(){
        

        Ray ray = new Ray(transform.position, new Vector3(-1 * range, 0, 0));
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red);

        if(Physics.Raycast(ray, out RaycastHit hit, range)){

            if (hit.collider.tag == "Destroyable" && !firstCheck){

                firstCheck = true;
                print("collision detected");
                hit.collider.gameObject.SetActive(false);
        
            }
            
        } 
        else firstCheck = false;
    }

}
