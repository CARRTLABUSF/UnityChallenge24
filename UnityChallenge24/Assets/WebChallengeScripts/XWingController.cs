using UnityEngine;

public class XWingController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 20f;                // Speed of movement
    public Vector3 boundsMin = new Vector3(-500f, -500f, -500f);  // Min bounds
    public Vector3 boundsMax = new Vector3(500f, 500f, 500f);     // Max bounds
    public float minimumDistance = 100f;    // Minimum distance to new target
    public float rotationSpeed = 2f;        // Speed of rotation when changing direction
    public float acceleration = 10f;        // Acceleration applied to reach target speed
    public float dampingFactor = 0.98f;     // Damping factor to smooth out movement

    [Header("Roll Settings")]
    public float rollAmount = 30f;          // Maximum roll angle
    public float rollDuration = 1f;         // Duration of each roll
    public float minTimeBetweenRolls = 2f;  // Minimum time between rolls
    public float maxTimeBetweenRolls = 5f;  // Maximum time between rolls

    private Rigidbody rb;                   // Rigidbody reference
    private Vector3 targetPoint;            // Current target point
    private Quaternion targetRotation;      // Target rotation
    private float rollTimer;                // Timer for when to perform a roll
    private float rollEndTime;              // Time when the roll effect ends
    private float rollDirection;            // Direction of the roll (-1 or 1)

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody component found! Please add a Rigidbody to the object.");
            return;
        }

        rb.useGravity = false; // Ensure gravity is disabled for this object
        ChooseNewTargetPoint();
        ResetRollTimer();
    }

    void FixedUpdate()
    {
        if (rb == null) return;

        // Move towards the target point
        MoveToTarget();

        // Check if we need to pick a new target point
        if (Vector3.Distance(transform.position, targetPoint) < 5f)
        {
            ChooseNewTargetPoint();
        }

        // Handle rolling
        HandleRolling();

        // Apply damping to smooth out movement
        ApplyDamping();
    }

    void ChooseNewTargetPoint()
    {
        do
        {
            // Generate a random point within bounds
            targetPoint = new Vector3(
                Random.Range(boundsMin.x, boundsMax.x),
                Random.Range(boundsMin.y, boundsMax.y),
                Random.Range(boundsMin.z, boundsMax.z)
            );
        } while (Vector3.Distance(transform.position, targetPoint) < minimumDistance);
    }

    void MoveToTarget()
    {
        // Calculate direction to the target point
        Vector3 direction = (targetPoint - rb.position).normalized;

        // Calculate the desired velocity
        Vector3 desiredVelocity = direction * speed;

        // Apply force to gradually reach the desired velocity
        Vector3 force = (desiredVelocity - rb.velocity) * acceleration;
        rb.AddForce(force, ForceMode.Acceleration);

        // Smoothly rotate towards the target point
        targetRotation = Quaternion.LookRotation(direction);
        rb.MoveRotation(Quaternion.Lerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
    }

    void HandleRolling()
    {
        if (rb == null) return;

        // Update roll timer
        rollTimer -= Time.deltaTime;

        // Start a new roll if the timer reaches zero
        if (rollTimer <= 0)
        {
            rollEndTime = Time.time + rollDuration;
            rollDirection = Random.value > 0.5f ? 1f : -1f; // Randomly pick roll direction
            ResetRollTimer();
        }

        // Apply rolling effect if within the roll duration
        if (Time.time < rollEndTime)
        {
            float rollAngle = rollDirection * rollAmount;
            Quaternion rollRotation = Quaternion.Euler(rb.rotation.eulerAngles.x, rb.rotation.eulerAngles.y, rollAngle);
            rb.MoveRotation(Quaternion.Lerp(rb.rotation, rollRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }

    void ApplyDamping()
    {
        // Apply a damping factor to gradually reduce velocity to avoid oscillations
        rb.velocity *= dampingFactor;
    }

    void ResetRollTimer()
    {
        // Randomly pick the next roll time
        rollTimer = Random.Range(minTimeBetweenRolls, maxTimeBetweenRolls);
    }
}
