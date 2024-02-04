using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public float speed = 2f; // Speed of the movement
    public float distance = 3f; // Maximum distance the object moves from the starting point

    private Vector3 startPosition;

    void Start()
    {
        // Store the starting position of the GameObject
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the current offset using PingPong to create back-and-forth motion
        float offset = Mathf.PingPong(Time.time * speed, distance);

        // Apply the offset to the starting position
        Vector3 newPosition = startPosition + new Vector3(0, 0, offset);

        // Update the GameObject's position
        transform.position = newPosition;
    }
}
