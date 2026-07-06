using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerLook : MonoBehaviour
{
    [FormerlySerializedAs("mouseSense")] public float MouseSense = 200f;
    [FormerlySerializedAs("cam")] public Transform Cam;

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
        if (Cam == null)
        {
            return;
        }

        float mouseX = lookInput.x * MouseSense * Time.deltaTime;
        float mouseY = lookInput.y * MouseSense * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        Cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

}
