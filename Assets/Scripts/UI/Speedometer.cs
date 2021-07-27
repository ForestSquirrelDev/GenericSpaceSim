using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Shows speed of player ship.
/// </summary>
public class Speedometer : MonoBehaviour
{
    [SerializeField] Text speedText;
    [SerializeField] Ship ship;

    private void Awake()
    {
        if (ship == null)
        {
            Debug.LogWarning($"Field '{nameof(ship)}' is not set in the inspector. FindObjectOfType will be used instead.");
            ship = FindObjectOfType<Ship>();
        }
        if (speedText == null)
        {
            Debug.LogWarning($"Field '{nameof(speedText)}' is not set in the inspector. {gameObject.name} will be disabled.");
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        speedText.text = "Speed: " + ship.ShipMovement.CurrentSpeed.ToString(format: "0");
    }
}
