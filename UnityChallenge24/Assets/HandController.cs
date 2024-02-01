using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public float speed = 1.0f; // Speed of movement
    private float timer = 0.0f;
    private bool movingPositive = true;

    void Update()
    {
        timer += Time.deltaTime; // Increase timer with time passed

        // If timer reaches 10 seconds, change direction and reset timer
        if (timer >= 2f)
        {
            movingPositive = !movingPositive;
            timer = 0.0f;
        }

        // Move the object along the z-axis based on direction
        float direction = movingPositive ? 1.0f : -1.0f;
        transform.Translate(0.0f, 0.0f, direction * speed * Time.deltaTime);
    }
}
