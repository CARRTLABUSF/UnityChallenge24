using UnityEngine;

public class FlightMovement : MonoBehaviour
{
    public float speed = 5f; 
    public float rotationSpeed = 20f; 
    public float pitchSpeed = 10f; 
    public float rollSpeed = 10f;
    
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;

        // Automatic rotations for more dynamic flight
        float yawRotation = Mathf.Sin(Time.time) * rotationSpeed; // Yaw (Y-axis)
        float pitchRotation = Mathf.Cos(Time.time * 0.5f) * pitchSpeed; // Pitch (X-axis)
        float rollRotation = Mathf.Sin(Time.time * 0.3f) * rollSpeed; // Roll (Z-axis)

        // Apply rotations
        transform.Rotate(new Vector3(pitchRotation, yawRotation, rollRotation) * Time.fixedDeltaTime);
    }
}
