using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform target;

    [Range(1, 100)]
    [SerializeField] private float distance = 10.0f;

    [Range(1,200)]
    [SerializeField] private float rotationSpeed = 10.0f;

    [Range(0, 1)]
    [Tooltip("Bigger value -> camera reaches player slower.")]
    [SerializeField] private float smoothTime = 0.1f;

    private Vector3 cameraPos;
    private Vector3 smoothPos;

    private float angle;
    private Vector3 velocity;

    void LateUpdate()
    {
        // Calculate position.
        cameraPos = target.position - (target.forward * distance) + target.up * distance * 0.25f;
        smoothPos = Vector3.SmoothDamp(transform.position, cameraPos, ref velocity, smoothTime);

        transform.position = smoothPos;

        // Calculating the angle allows our camera to speed up or slow down depending on the difference between Quaternions.
        angle = Mathf.Abs(Quaternion.Angle(transform.rotation, target.rotation));

        transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, (rotationSpeed + angle) * Time.deltaTime);
    }
}
