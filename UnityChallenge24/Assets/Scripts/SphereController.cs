using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SphereController : MonoBehaviour
{
    [SerializeField] private int maxLives = 5;
    private int currentLives;
    private bool isDying = false;

    void Start()
    {
        currentLives = maxLives;
        Debug.Log($"Sphere starting with {currentLives} lives");
    }

    public void TakeDamage()
    {
        if (isDying) return;

        currentLives--;
        Debug.Log($"Hit! Lives remaining: {currentLives}");

        // Visual feedback when hit
        StartCoroutine(FlashRed());

        if (currentLives <= 0 && !isDying)
        {
            isDying = true;
            StartCoroutine(DeathSequence());
        }
    }

    private IEnumerator FlashRed()
    {
        Renderer renderer = GetComponent<Renderer>();
        Color originalColor = renderer.material.color;
        
        renderer.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        if (currentLives > 0) // Only restore color if still alive
        {
            renderer.material.color = originalColor;
        }
    }

    private IEnumerator DeathSequence()
    {
        Debug.Log("Sphere destroyed! Restarting in 5 seconds...");
        
        // Make the sphere red and then fade it out
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = Color.red;

        // Disable the collider immediately to prevent further hits
        GetComponent<Collider>().enabled = false;

        float elapsed = 0f;
        float duration = 1f;
        Color startColor = renderer.material.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            renderer.material.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        renderer.enabled = false;
        
        // Wait the remaining time before restarting
        yield return new WaitForSeconds(4f);
        
        // Restart the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}