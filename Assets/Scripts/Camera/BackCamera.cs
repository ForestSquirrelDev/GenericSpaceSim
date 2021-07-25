using UnityEngine;

/// <summary>
/// Makes camera seem following a little behind and above the ship to make player's flight more entertaining and responsive.
/// Based on the camera script from https://www.youtube.com/watch?v=f1jGPdT4Er. Great thanks to Bit Galaxis for showing and overwiew of it.
/// </summary>
public class BackCamera : MonoBehaviour
{
    [Tooltip("Target to follow.")]
    [SerializeField] private Transform target;

    [Range(0.01f, 0.5f)]
    [Tooltip("How high above the ship should camera be placed.")]
    [SerializeField] private float cameraHeight = 0.25f;

    [Range(1.0f, 100.0f)]
    [Tooltip("How far from target will camera fly (Z world axis).")]
    [SerializeField] private float distance = 12.0f;

    [Range(1.0f, 10.0f)]
    [Tooltip("How fast camera changes its rotation according to player ship.")]
    [SerializeField] private float rotationSpeed = 3.0f;

    [Range(0.01f, 1.0f)]
    [Tooltip("Bigger value -> camera reaches player slower (Z world axis).")]
    [SerializeField] private float smoothTime = 0.2f;

    private Vector3 cameraPos;
    private Vector3 velocity;

    private Transform thisTransform;

    private float angle;

    private void Awake()
    {
        if(target == null)
        {
            Debug.LogWarning($"Field '{nameof(target)}' is not set in the inspector for {gameObject.name}. Script will be disabled");
            GetComponent<BackCamera>().enabled = false;
        }
    }

    private void Start() => thisTransform = gameObject.transform;

    void LateUpdate()
    {
        FollowPlayer();
    }

    /// <summary>
    /// This is the best part.
    /// Calculating the angle allows our camera to change its velocity depending on the difference between Quaternions.
    /// Basically this works like a custom Lerp - camera speeds up trying to catch up to the ship and slows down in the end, 
    /// which effectively prevents jittery rotation patterns and makes camera ultra smooth (built-in Lerp and Slerp wouldn't work properly).
    /// </summary>
    private void FollowPlayer()
    {
        // Calculate position.
        cameraPos = target.position - (target.forward * distance) + target.up * distance * cameraHeight;

        thisTransform.position = Vector3.SmoothDamp(thisTransform.position, cameraPos, ref velocity, smoothTime);

        // Calculate angle for rotation smoothing.
        angle = Mathf.Abs(Quaternion.Angle(thisTransform.rotation, target.rotation));

        thisTransform.rotation = Quaternion.RotateTowards(from: thisTransform.rotation,
                                                          to: target.rotation,
                                                          maxDegreesDelta: (angle * rotationSpeed) * Time.deltaTime);
    }
}
