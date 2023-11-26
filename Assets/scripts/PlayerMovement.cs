using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 10f;
    public float groundDrag = 5f;

    [Header("Jump")]
    public float jumpForce = 6f;
    public float jumpCooldown = 0.3f;
    public float airMultiplier = 0.4f;
    private bool readyToJump;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    private bool isGrounded;
    public Transform orientation;
    private float horizontal;
    private float vertical;
    private Vector3 moveDirection;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        PlayerInputs();
        SpeedControl();

        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void PlayerInputs()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Space) && readyToJump && isGrounded)
        {
            readyToJump = false;
            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * vertical + orientation.right * horizontal;

        if (isGrounded)
        {

            rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * speed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}