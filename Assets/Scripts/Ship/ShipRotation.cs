using UnityEngine;

/// <summary>
/// This class is responsible for rotating ship by mouse and E + Q buttons.
/// </summary>
public class ShipRotation : MonoBehaviour
{
    [Range(1.0f, 300.0f)]
    [Tooltip("How fast the ship is reacting to mouse position.")]
    [SerializeField] private float rotationSpeed = 120.0f;

    [Range(1.0f, 200.0f)]
    [Tooltip("How fast the ship turns by Z axis (i.e. rolls).")]
    [SerializeField] private float deltaRollSpeed = 100.0f;

    [Range(1.0f, 200.0f)]
    [Tooltip("Current roll speed will be clamped between this value and its negative representation.")]
    [SerializeField] private float maxRollSpeed = 100.0f;

    [Range(1.0f, 10.0f)]
    [Tooltip("Power of inertia for roll. The bigger this value is, the faster ship stops rolling after key is released.")]
    [SerializeField] private float inertia = 1.5f;

    [Range(0.01f, 1.0f)]
    [Tooltip("Amount of smoothing for roll. Smaller value -> more smoothed motion.")]
    [SerializeField] private float rollLerpTime = 0.4f;

    [SerializeField] private Ship ship;

    // Current speed of rotation by Z axis.
    private float currRollSpeed = 0f;

    private Transform thisTransform;

    private void Awake()
    {
        if (ship == null)
            ship = GetComponent<Ship>();
    }

    private void Start() => thisTransform = gameObject.transform;

    private void Update()
    {
        HandleRotation();
        HandleTilting();
    }

    private void HandleRotation()
    {
        // Rotate ship's Transform by X and Y axes according to mouse position input.
        thisTransform.Rotate(xAngle: (ship.ShipInput.Pitch * rotationSpeed) * Time.deltaTime,
                             yAngle: (ship.ShipInput.Yaw * rotationSpeed) * Time.deltaTime,
                             zAngle: 0f);
    }

    /// <summary>
    /// Same idea as with the movement. Rotate over Z axis every frame by a value that's changed by input.
    /// Then slowly decrease this value to imitate inertia.
    /// </summary>
    private void HandleTilting()
    {
        if (currRollSpeed > 0f || currRollSpeed < 0f)
            thisTransform.Rotate(0f, 0f, currRollSpeed * Time.deltaTime);

        if (ship.ShipInput.QIsPressed)
            currRollSpeed += Time.deltaTime * deltaRollSpeed;
        else if (ship.ShipInput.EIsPressed)
            currRollSpeed -= Time.deltaTime * deltaRollSpeed;

        else if (currRollSpeed > 0f)
            currRollSpeed = Mathf.Lerp(a: currRollSpeed,
                                       b: currRollSpeed - Time.deltaTime * deltaRollSpeed * inertia,
                                       t: rollLerpTime);
        else if (currRollSpeed < 0f)
            currRollSpeed = Mathf.Lerp(a: currRollSpeed,
                                       b: currRollSpeed + Time.deltaTime * deltaRollSpeed * inertia,
                                       t: rollLerpTime);

        currRollSpeed = Mathf.Clamp(currRollSpeed, -maxRollSpeed, maxRollSpeed);
    }
}
