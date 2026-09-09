using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Applies the rules tied to the player's size without changing the base BOB scripts.
/// The player collects weapons while large and can fire them only while small.
/// </summary>
public class PlayerFormController : MonoBehaviour
{
    [SerializeField, Range(0.1f, 0.95f)] private float smallScaleThreshold = 0.75f;
    [SerializeField] private Camera playerCamera;
    [SerializeField, Min(0.1f)] private float pickupRange = 3f;

    private PlayerShooting playerShooting;
    private float startingScaleMagnitude;
    private bool formStateInitialized;

    public bool IsSmall { get; private set; }

    private void Awake()
    {
        startingScaleMagnitude = transform.localScale.magnitude;
        playerShooting = GetComponent<PlayerShooting>();

        if (playerCamera == null)
        {
            playerCamera = GetComponentInChildren<Camera>();
        }

        RefreshFormState();
    }

    private void Update()
    {
        RefreshFormState();
    }

    // Called by the PlayerInput component using its Send Messages behaviour.
    private void OnPickUp(InputValue _)
    {
        if (IsSmall || playerCamera == null)
        {
            return;
        }

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
        {
            PickUp pickup = hit.collider.GetComponentInParent<PickUp>();
            if (pickup != null)
            {
                pickup.OnPickUp();
            }
        }
    }

    private void RefreshFormState()
    {
        if (startingScaleMagnitude <= Mathf.Epsilon)
        {
            return;
        }

        bool shouldBeSmall = transform.localScale.magnitude / startingScaleMagnitude < smallScaleThreshold;
        if (formStateInitialized && IsSmall == shouldBeSmall)
        {
            return;
        }

        IsSmall = shouldBeSmall;
        formStateInitialized = true;
        if (playerShooting != null)
        {
            playerShooting.enabled = IsSmall;
        }
    }
}
