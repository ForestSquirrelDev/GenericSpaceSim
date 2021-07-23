using UnityEngine;

/// <summary>
/// Transform-based movement by Z world axis.
/// </summary>
public class ShipMovement : MonoBehaviour
{
    [SerializeField] private float currentSpeed = 0f;

    [Range(0, 300)]
    [SerializeField] private float maxSpeed = 200.0f;

    [Range(0, 200)]
    [Tooltip("How fast the ship's position is changed through time.\nThe less this value is, the smoother (or heavier) movement feels.")]
    [SerializeField] private float deltaMovementSpeed = 25.0f;

    [Range(0, 100)]
    [Tooltip("How fast the ship will be losing its speed over time when not accelerating.\nBigger value -> ship will save speed longer.")]
    [SerializeField] private float inertia = 5.0f;

    [SerializeField] private Ship ship;

    private Transform thisTransform;

    private Vector3 targetPos;
    private Vector3 smoothedPos;

    // Accessor for modifying speed by external forces.
    public float CurrentSpeed
    {
        get => currentSpeed;
        internal set => currentSpeed = value;
    }

    private void Awake()
    {
        if (ship == null)
            ship = GetComponent<Ship>();
    }

    private void Start() => thisTransform = gameObject.transform;
    
    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        targetPos = (thisTransform.position + (thisTransform.forward * (currentSpeed)) * Time.deltaTime);
        smoothedPos = Vector3.Lerp(thisTransform.position, targetPos, 1.0f);

        // Moving ship by applying changes directly to its transform every frame. 
        thisTransform.position = smoothedPos;

        // Smoothly changing the apply value over time on key presses.
        if (ShipInput.WIsPressed)
            currentSpeed += deltaMovementSpeed * Time.deltaTime;
        else if (ShipInput.SIsPressed)
            currentSpeed -= (deltaMovementSpeed * 1.5f) * Time.deltaTime;

        // Mimic inertia force by passive speed decrease.
        else if (currentSpeed > 0.0001f)
            currentSpeed -= (deltaMovementSpeed / inertia) * Time.deltaTime;
        else if (currentSpeed < 0f)
            currentSpeed += (deltaMovementSpeed / inertia) * Time.deltaTime;

        // Setting speed limits.
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed / 3.0f, maxSpeed);
    }
}
