/*using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine;

public class LaserHand : MonoBehaviour
{
    public string targetTag = "DestroyObject"; // Tag to check for
    public float raycastDistance = 10f; // Distance of the raycast
    private Dictionary<Collider, int> hitCounts = new Dictionary<Collider, int>(); // Dictionary to track hit counts for each object

    void Update()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position; // Ray origin from object's position
        Vector3 rayDirection = transform.forward; // Ray direction (forward of the object)

        // Raycast from the object's position in the forward direction
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, raycastDistance))
        {
            // Draw the ray in the editor for debugging purposes
            Debug.DrawRay(rayOrigin, rayDirection * raycastDistance, Color.red);

            // Check if the hit object has the target tag
            if (hit.collider.CompareTag(targetTag))
            {
                // Log raycast hit details for debugging
                Debug.Log("Ray hit: " + hit.collider.name + " with tag: " + targetTag);

                // Check if we already have an entry for this object in the dictionary
                if (!hitCounts.ContainsKey(hit.collider))
                {
                    hitCounts[hit.collider] = 0; // Initialize hit count for this object
                }

                // Increment the hit count for this specific object
                hitCounts[hit.collider]++;
                Debug.Log("Hit count for " + hit.collider.name + ": " + hitCounts[hit.collider]);

                // If the raycast has hit the object 3 times, call the method to decrement lives
                if (hitCounts[hit.collider] >= 3)
                {
                    // Check if the hit object has a SphereController attached
                    SphereController sphereController = hit.collider.GetComponent<SphereController>();
                    if (sphereController != null)
                    {
                        // Call the method to decrement lives in SphereController
                        sphereController.DecrementLives();
                    }

                    // Reset the hit count for this object after triggering the life decrement
                    hitCounts[hit.collider] = 0;
                }
            }
        }
        else
        {
            // Draw the ray in the editor even when there's no hit
            Debug.DrawRay(rayOrigin, rayDirection * raycastDistance, Color.green);
        }
    }
}
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHand : MonoBehaviour
{
    public string targetTag = "DestroyObject"; // Tag to check for
    public float raycastDistance = 10f; // Distance of the raycast
    private Dictionary<Collider, int> hitCounts = new Dictionary<Collider, int>(); // Dictionary to track hit counts for each object
    public GameManager gameManager;
    public Camera mainCamera;  // Reference to the main camera
    public LineRenderer lineRenderer; // LineRenderer component

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;  // Assign Main Camera if not set
        }

        // Set up the LineRenderer
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();  // Add LineRenderer if not already attached
        }

        lineRenderer.startWidth = 0.1f;  // Set the width of the line
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));  // Set a simple material for the line
        lineRenderer.startColor = Color.red;  // Start color of the ray
        lineRenderer.endColor = Color.red;  // End color of the ray
        lineRenderer.positionCount = 2;  // Two positions for a start and end
    }

    void Update()
    {
        RaycastHit hit;

        // Get the world position of the mouse in 3D space, assuming the ray should point in that direction
        Vector3 mousePosition = Input.mousePosition;  // Get cursor position
        mousePosition.z = raycastDistance;  // Distance from the camera to the target

        Vector3 targetPosition = mainCamera.ScreenToWorldPoint(mousePosition);  // Convert to world position
        Vector3 rayOrigin = transform.position;  // Ray origin from the object's position
        Vector3 rayDirection = (targetPosition - rayOrigin).normalized;  // Direction towards the mouse position
                                                                         // Update LineRenderer positions
        lineRenderer.SetPosition(0, rayOrigin);  // Set the start of the ray
        lineRenderer.SetPosition(1, rayOrigin + rayDirection * raycastDistance);

        // Raycast from the object's position towards the mouse/target
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, raycastDistance))
        {
            // Draw the ray in the editor for debugging purposes
            Debug.DrawRay(rayOrigin, rayDirection * raycastDistance, Color.red);

            // Check if the hit object has the target tag
            if (hit.collider.CompareTag(targetTag))
            {
                // Log raycast hit details for debugging
                Debug.Log("Ray hit: " + hit.collider.name + " with tag: " + targetTag);

                // Check if we already have an entry for this object in the dictionary
                if (!hitCounts.ContainsKey(hit.collider))
                {
                    hitCounts[hit.collider] = 0; // Initialize hit count for this object
                }

                // Increment the hit count for this specific object
                hitCounts[hit.collider]++;
                Debug.Log("Hit count for " + hit.collider.name + ": " + hitCounts[hit.collider]);

                // If the raycast has hit the object 3 times, call the method to decrement lives
                if (hitCounts[hit.collider] >= 3)
                {
                    // Check if the hit object has a SphereController attached
                    SphereController sphereController = hit.collider.GetComponent<SphereController>();
                    if (sphereController != null)
                    {
                        // Call the method to decrement lives in SphereController
                        sphereController.DecrementLives();
                        
                    }

                    // Reset the hit count for this object after triggering the life decrement
                    hitCounts[hit.collider] = 0;
                }
            }
        }
        else
        {
            // Draw the ray in the editor even when there's no hit
            Debug.DrawRay(rayOrigin, rayDirection * raycastDistance, Color.green);
        }
    }
    
}
