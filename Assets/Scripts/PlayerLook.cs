using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    public float mouseSense = 200f;
    public Transform cam;

    private float xRotation = 0;
    private Vector2 lookInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        ReadMouseInput();
        HandleMouseLook();
    }
    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    void ReadMouseInput()
    {
        if (Mouse.current == null)
        {
            return;
        }

        lookInput = Mouse.current.delta.ReadValue();
    }

    void HandleMouseLook()
    {
        if (cam == null)
        {
            return;
        }

        float mouseX = lookInput.x * mouseSense * Time.deltaTime;
        float mouseY = lookInput.y * mouseSense * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

}
