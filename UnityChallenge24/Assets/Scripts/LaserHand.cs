using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHand : MonoBehaviour
{
    public float raycastDistance = 20f; 
    

    void Update()
    {
        
        Ray ray = new Ray(transform.position,  transform.right*-1);
        
        
        RaycastHit hit;

        
        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            if (hit.collider.CompareTag("Destroyable")){
                SphereController sphereController = hit.collider.GetComponent<SphereController>();

                if (sphereController != null)
                {
                    
                    sphereController.ifHit();
                }
            }
        }
        Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red);
    }
}
