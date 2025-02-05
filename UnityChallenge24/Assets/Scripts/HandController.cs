using UnityEngine;

public class HandController : MonoBehaviour
{
    public float speed = 2.0f; 
    public float distance = 5.0f;
    private Vector3 startPosition;
    private LineRenderer lineRenderer;

    void Start()
    {
        startPosition = transform.position; // Store the initial position of the hand
        
        // Add LineRenderer dynamically
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }      
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
        lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        lineRenderer.material.color = Color.red;
    }

    void Update()
    {
        // Move the hand along the X-axis
        float movement = Mathf.PingPong(Time.time * speed, distance);
        transform.position = startPosition + new Vector3(movement, 0, 0);
        // Update laser positions
        Vector3 laserStart = transform.position;
        Vector3 laserEnd = transform.position + Vector3.forward * 10f; 
        lineRenderer.SetPosition(0, laserStart);
        lineRenderer.SetPosition(1, laserEnd);
    }
}
