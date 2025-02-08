using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    public int lives = 5; // Sphere starts with 5 lives

    void Start()
    {
        Debug.Log("Sphere Initialized with " + lives + " lives.");
    }

    public void TakeDamage()
    {
        lives--;
        Debug.Log("Sphere hit! Lives remaining: " + lives);

        if (lives <= 0)
        {
            StartCoroutine(DestroySphere());
        }
    }

    IEnumerator DestroySphere()
    {
        Debug.Log("Sphere destroyed!");
        yield return new WaitForSeconds(1f); // Small delay before disappearance
        Destroy(gameObject);
    }
}
