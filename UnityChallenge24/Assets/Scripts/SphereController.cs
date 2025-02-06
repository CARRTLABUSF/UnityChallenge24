using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    public int lives = 3; // Number of lives remaining
    // Call this method to decrement lives when the raycast hits the target 3 times
    public void DecrementLives()
    {
        lives--;
        Debug.Log("Lives remaining: " + lives);

        // Destroy the object if lives reach 0
        if (lives <= 0)
        {
            Debug.Log("No lives remaining. Destroying object.");
            Destroy(gameObject);
        }
    }
}
