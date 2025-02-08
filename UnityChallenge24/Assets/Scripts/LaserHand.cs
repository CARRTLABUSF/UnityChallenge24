using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHand : MonoBehaviour
{
    public float moveSpeed = 1f;
    private float leftLimitZ;   // Store the initial starting position as left limit
    public float rightLimitZ = 1f; // Right limit to 1
    private bool movingRight = true;

    public LayerMask sphereLayer;

    void Start()
    {
        leftLimitZ = transform.position.z; // Capture the starting position as the left limit
        sphereLayer = LayerMask.GetMask("Default");
    }

    void Update()
    {
        MoveHand();
        CheckForSphere();
    }

    void MoveHand()
    {
        // Move left and right along the Z-axis
        if (movingRight)
        {
            transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
            if (transform.position.z >= rightLimitZ)
                movingRight = false;  // Move back to left limit
        }
        else
        {
            transform.position -= Vector3.forward * moveSpeed * Time.deltaTime;
            if (transform.position.z <= leftLimitZ)
                movingRight = true;  // Move right again
        }
    }

    void CheckForSphere()
    {
        RaycastHit hit;
        Vector3 rayDirection = -Vector3.up;  // Cast ray downwards

        if (Physics.Raycast(transform.position, rayDirection, out hit, 10f))
        {
            Debug.Log("Raycast Hit: " + hit.collider.gameObject.name);

            if (hit.collider.CompareTag("Destroyable"))
            {
                Debug.Log("Hand hit the sphere! Calling TakeDamage()");
                SphereController sphere = hit.collider.GetComponent<SphereController>();
                if (sphere != null)
                {
                    sphere.TakeDamage();
                }
            }
        }
    }

}
