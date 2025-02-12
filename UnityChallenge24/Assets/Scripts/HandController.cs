using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public Camera mainCamera;  // Reference to the main camera
    public float depthOffset = 5f;  // Closer to the camera (adjust as needed)
    public float smoothSpeed = 15f;  // Movement smoothness

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;  // Assign Main Camera if not set
        }
    }

    void Update()
    {
        FollowCursor();
    }

    void FollowCursor()
    {
        Vector3 mousePosition = Input.mousePosition;  // Get cursor position
        mousePosition.z = mainCamera.nearClipPlane + depthOffset;  // Keep it closer to the camera

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);  // Convert to world space

        // Smooth movement
        transform.position = Vector3.Lerp(transform.position, worldPosition, Time.deltaTime * smoothSpeed);
    }
}
