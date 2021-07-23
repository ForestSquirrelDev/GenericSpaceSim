using UnityEngine;

/// <summary>
/// This script helps Unity physics engine deal with collisions, since we're using Transform-based movement.
/// </summary>
public class ShipCollisions : MonoBehaviour
{
    [Range(0, 1)]
    [Tooltip("How fast ship will lose its speed on collision. Bigger value -> faster speed drop.\nSmall values (approximately < 0.5f) will cause passing through colliders.")]
    [SerializeField] private float onCollisionSpeedDrop = 0.6f;

    [SerializeField] private Ship ship;

    private void Awake()
    {
        if (ship == null)
            ship = GetComponent<Ship>();
    }

    private void OnCollisionEnter(Collision other)
    {
        Rigidbody otherRigidbody = other.collider.attachedRigidbody;

        // Reducing speed on collision to prevent ship from passing through objects.
        if (otherRigidbody != null)
            ship.ShipMovement.CurrentSpeed = Mathf.Lerp (a: ship.ShipMovement.CurrentSpeed,
                                                         b: ship.ShipMovement.CurrentSpeed /= otherRigidbody.mass,
                                                         t: onCollisionSpeedDrop);
    }
}
