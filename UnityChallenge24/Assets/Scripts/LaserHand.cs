using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHand : MonoBehaviour
{
    [Header("Raycast")]
    [SerializeField] bool displayRaycast;
    [SerializeField] float range;

    //prevents sending multiple OnRaycastHit message to the recipient while Raycast is passing through
    //could be change to SphereController if specific Destroy() are needed
    private GameObject pastHitObject = null;

    void Update()
    {
        LaserHandDestroySphere();
    }

    private void LaserHandDestroySphere()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, range)) {
            GameObject hitObject = hit.collider.gameObject;

            //if object is destroyable AND isn't in the process of being hit by the current Raycast
            if (hitObject.CompareTag("Destroyable") && hitObject != pastHitObject)
            {
                pastHitObject = hitObject;
                HandleEnemyHit(hitObject);
            }
        } else {
            pastHitObject = null;
        }
            
        if (this.displayRaycast) {
            Debug.DrawLine(transform.position, Vector3.forward * range, Color.red);
        }
    }

    //handle sphere controller
    private void HandleEnemyHit(GameObject hitObject)
    {
        SphereController controller = hitObject.GetComponent<SphereController>();
        if (controller != null) {
            controller.OnRaycastHit();
            if (controller.IsDead()) {
                StartCoroutine(controller.Dies());
            } 
        }
    }
}
