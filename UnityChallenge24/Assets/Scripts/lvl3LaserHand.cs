using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class lvl3LaserHand : MonoBehaviour
{
    [SerializeField] public GameObject _sphere;
    // range holds how far raycast will go
    float range = 3;
    // firstcheck will be used to keep track of first collison of gameobject and raycast
    bool firstCheck = false;
    
    // Update is called once per frame
    void Update()
    {   //calling method so it updates
        checkCollision();
    }

    void checkCollision(){
        Ray ray = new Ray(transform.position, new Vector3(-1 * range, 0, 0));
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red);

        if(Physics.Raycast(ray, out RaycastHit hit, range)){

            //if statement used recognize first raycast collison for spehere so it keep checking every frame
            if (hit.collider.tag == "Destroyable" && !firstCheck){

                firstCheck = true;
                var health = _sphere.GetComponent<SphereController>();
                health.HP -= 1;
                print("Damage Taken! HP: " + health.HP);
                
        
            }
            
        } 
        //first check turned false when no longer colliding with sphere
        else firstCheck = false;
    }

}
