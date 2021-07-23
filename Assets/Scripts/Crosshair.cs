using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite crosshair;
    void Update()
    {
        image.transform.position = Input.mousePosition;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
