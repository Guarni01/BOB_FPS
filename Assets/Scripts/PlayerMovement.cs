using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float JumpForce = 5f;
    public float MoveSpeed = 5f;

    public Transform GroundCheck;
    public float GroundDistance = 0.4f;
    public LayerMask GroundMask;
    
    public Animator PlayerAnimator; 

    private Rigidbody rb;
    private Vector2 moveInput;
    private bool isGrounded;
    private PlayerInput playerInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        playerInput = new PlayerInput();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();

        
        if (moveInput.magnitude > 0.1f)
        {
            PlayerAnimator.SetBool("Walking", true);
        }
        else
        {
            PlayerAnimator.SetBool("Walking", false);
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void OnJump()
    {
        if(isGrounded)
        {
            rb.AddForce(new Vector3(0, JumpForce, 0), ForceMode.Impulse);
            
            
            PlayerAnimator.SetTrigger("Jump"); 
        }
    }
    
    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);
    }

    void OnMovement(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void MovePlayer()
    {
        Vector3 direction = transform.right * moveInput.x + transform.forward * moveInput.y;
        direction.Normalize();
        rb.linearVelocity = new Vector3(direction.x * MoveSpeed, rb.linearVelocity.y, direction.z * MoveSpeed);
    }
}