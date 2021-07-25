using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Replaces cursor with crosshair image, that reflects mouse position.
/// </summary>
public class Crosshair : MonoBehaviour
{
    [SerializeField] private Image crosshair;

    private void Awake()
    {
        if (crosshair == null)
        {
            Debug.LogWarning($"Field '{nameof(crosshair)}' is not set in the inspector. {gameObject.name} will be disabled.");
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        Cursor.visible = false;
        crosshair.transform.position = Input.mousePosition;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
