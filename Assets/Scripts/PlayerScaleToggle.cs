using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScaleToggle : MonoBehaviour
{
    public float scaleMultiplier = 1f / 3f;

    private Vector3 originalScale;
    private bool isScaledDown;
    private PlayerInput playerInput;

    void Awake()
    {
        originalScale = transform.localScale;
        playerInput = new PlayerInput();
    }

    void OnEnable()
    {
        playerInput.Little_Big.Enable();
        playerInput.Little_Big.ScaleToggle.performed += OnScaleToggle;
    }

    void OnDisable()
    {
        playerInput.Little_Big.ScaleToggle.performed -= OnScaleToggle;
        playerInput.Little_Big.Disable();
    }

    void OnScaleToggle(InputAction.CallbackContext context)
    {
        ToggleScale();
    }

    void ToggleScale()
    {
        isScaledDown = !isScaledDown;
        transform.localScale = isScaledDown ? originalScale * scaleMultiplier : originalScale;
    }
}
