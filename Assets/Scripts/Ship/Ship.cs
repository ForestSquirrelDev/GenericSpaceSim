using UnityEngine;

/// <summary>
/// This script is responsible for communication between other player classes.
/// Instead of communicating directly between each other, player scripts connect through this bus script
/// to increase manageability of code.
/// </summary>
[RequireComponent(typeof(ShipInput))]
[RequireComponent(typeof(ShipMovement))]
[RequireComponent(typeof(ShipRotation))]
[RequireComponent(typeof(ShipCollisions))]
public class Ship : MonoBehaviour
{
    [SerializeField] private ShipInput shipInput;
    [SerializeField] private ShipMovement shipMovement;
    [SerializeField] private ShipRotation shipRotation;
    [SerializeField] private ShipCollisions shipCollisions;

    public ShipInput ShipInput => shipInput;
    public ShipMovement ShipMovement => shipMovement;
    public ShipRotation ShipRotation => shipRotation;
    public ShipCollisions ShipCollisions => shipCollisions;

    private void Awake()
    {
        if (shipInput == null)
            shipInput = GetComponent<ShipInput>();
        if (shipMovement == null)
            shipMovement = GetComponent<ShipMovement>();
        if (shipRotation == null)
            shipRotation = GetComponent<ShipRotation>();
        if (shipCollisions == null)
            shipCollisions = GetComponent<ShipCollisions>();
    }
}
