using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Shows speed of player ship.
/// </summary>
public class Speedometer : MonoBehaviour
{
    [SerializeField] Text speed;
    [SerializeField] Ship ship;

    private void Awake()
    {
        if (ship == null)
        {
            Debug.LogWarning($"Field '{nameof(ship)}' is not set in the inspector. FindObjectOfType will be used instead.");
            ship = FindObjectOfType<Ship>();
        }
        if (speed == null)
        {
            Debug.LogWarning($"Field '{nameof(speed)}' is not set in the inspector. {gameObject.name} will be disabled.");
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        speed.text = "Speed: " + Mathf.Round(ship.ShipMovement.CurrentSpeed).ToString();
    }
}
