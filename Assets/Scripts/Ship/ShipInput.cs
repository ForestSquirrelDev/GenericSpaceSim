using UnityEngine;

public class ShipInput : MonoBehaviour
{
    // Mouse position relative to screen.
    [Range(-1, 1)]
    [SerializeField] private float pitch;
    [Range(-1, 1)]
    [SerializeField] private float yaw;
    
    private bool wIsPressed;
    private bool sIsPressed;

    private bool qIsPressed;
    private bool eIsPressed;

    // A bunch of static getters for any class interested in input information.
    public static float Pitch => instance.pitch;
    public static float Yaw => instance.yaw;

    public static bool WIsPressed => instance.wIsPressed;
    public static bool SIsPressed => instance.sIsPressed;
    public static bool QIsPressed => instance.qIsPressed;
    public static bool EIsPressed => instance.eIsPressed;

    private static ShipInput instance;

    private void Awake() => instance = this;

    void Update()
    {
        SetStickCommandsUsingMouse();
        HandleKeyboardInput();
    }

    /// <summary>
    /// Imitate virtual joystick to rotate ship by mouse position on the screen.
    /// </summary>
    private void SetStickCommandsUsingMouse()
    {
        Vector3 mousePos = Input.mousePosition;

        // Figure out mouse position relative to center of screen.
        // (0, 0) is center, (-1, -1) is bottom left, (1, 1) is top right.      
        pitch = (mousePos.y - (Screen.height * 0.5f)) / (Screen.height * 0.5f);
        yaw = (mousePos.x - (Screen.width * 0.5f)) / (Screen.width * 0.5f);

        // Make sure the values don't exceed limits.
        pitch = -Mathf.Clamp(pitch, -1.0f, 1.0f);
        yaw = Mathf.Clamp(yaw, -1.0f, 1.0f);
    }

    private void HandleKeyboardInput()
    {
        if (Input.GetKey(KeyCode.W))
            wIsPressed = true;
        else
            wIsPressed = false;

        if (Input.GetKey(KeyCode.S))
            sIsPressed = true;
        else
            sIsPressed = false;

        if (Input.GetKey(KeyCode.Q))
            qIsPressed = true;
        else
            qIsPressed = false;

        if (Input.GetKey(KeyCode.E))
            eIsPressed = true;
        else
            eIsPressed = false;
    }
}
