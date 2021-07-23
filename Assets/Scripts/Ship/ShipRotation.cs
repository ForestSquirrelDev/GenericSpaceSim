using UnityEngine;

/// <summary>
/// This class is responsible for rotating ship by mouse and E + Q buttons.
/// </summary>
public class ShipRotation : MonoBehaviour
{
    [Range(0, 300)]
    [Tooltip("How fast the ship is reacting to mouse position.")]
    [SerializeField] private float rotationSpeed = 120.0f;

    [Tooltip("Current speed of rotation by Z axis (i.e. tilting).")]
    [SerializeField] private float currTiltSpeed = 0f;

    [Range(0, 200)]
    [Tooltip("How fast the ship turns by Z axis.")]
    [SerializeField] private float deltaTiltSpeed = 100.0f;

    [Range(0, 200)]
    [Tooltip("Current tilt speed will be clamped between this value and its negative representation.")]
    [SerializeField] private float maxTiltSpeed = 100.0f;

    [SerializeField] private Ship ship;

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
        thisTransform.Rotate
            ((ShipInput.Pitch * rotationSpeed) * Time.deltaTime,
            (ShipInput.Yaw * rotationSpeed) * Time.deltaTime,
            0f);
    }

    /// <summary>
    /// Same idea as with the movement. Rotate over Z axis every frame by a value that's changed by input.
    /// Then slowly decrease this value to imitate inertia.
    /// </summary>
    private void HandleTilting()
    {
        if (currTiltSpeed > 0f || currTiltSpeed < 0f)
            transform.Rotate(0f, 0f, currTiltSpeed * Time.deltaTime);

        if (ShipInput.QIsPressed)
            currTiltSpeed += Time.deltaTime * deltaTiltSpeed;
        else if (ShipInput.EIsPressed)
            currTiltSpeed -= Time.deltaTime * deltaTiltSpeed;

        else if (currTiltSpeed > 0f)
            currTiltSpeed = Mathf.Lerp(currTiltSpeed,
                                       currTiltSpeed - Time.deltaTime * deltaTiltSpeed * 1.5f,
                                       0.4f);
        else if (currTiltSpeed < 0f)
            currTiltSpeed = Mathf.Lerp(currTiltSpeed,
                                       currTiltSpeed + Time.deltaTime * deltaTiltSpeed * 1.5f,
                                       0.4f);

        currTiltSpeed = Mathf.Clamp(currTiltSpeed, -maxTiltSpeed, maxTiltSpeed);
    }
}
