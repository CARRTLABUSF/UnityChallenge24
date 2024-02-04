using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHand : MonoBehaviour
{
    public float rayLength = 5f; // Length of the ray
    private bool isHittingGameObject = false; // Flag to track if currently hitting the sphere

    void Update()
    {
        // Cast a ray forward from the hand's position
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(new Vector3(-1, 0, 0)) * rayLength;
        Debug.DrawRay(transform.position, forward, Color.red); // Visualize the ray in the editor

        if (Physics.Raycast(transform.position, forward, out hit, rayLength))
        {
            if (hit.collider.gameObject.CompareTag("Destroyable"))
            {
                if (!isHittingGameObject) // If it wasn't already hitting the sphere
                {
                    // Remove one life from the GameObject that was hit
                    hit.collider.gameObject.GetComponent<SphereController>().removeLife();
                    if (hit.collider.gameObject.GetComponent<SphereController>().getLives() <= 0)
                    {
                        Destroy(hit.collider.gameObject);
                        Debug.Log("Destroyed " + hit.collider.gameObject.name);
                    }
                    else
                    {
                        Debug.Log("Damage Taken! Life: " + hit.collider.gameObject.GetComponent<SphereController>().getLives());
                    }
                    isHittingGameObject = true; // Set flag to true
                }
            }
            else
            {
                // If the ray is not hitting the sphere object anymore
                if (isHittingGameObject)
                {
                    isHittingGameObject = false;
                }
            }
        }
    }
}
