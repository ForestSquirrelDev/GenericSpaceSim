using UnityEngine;

/// <summary>
/// Applies random angular velocity to an asteroid at the start of game.
/// </summary>
public class RandomRotator : MonoBehaviour
{
    [SerializeField] private float tumble;

    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
    }
}