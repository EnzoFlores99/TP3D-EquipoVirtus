using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 10f;
    public float jumpForce = 10f;
    public float dashForce = 30f;
    private bool canDash = false;
    private float groundDistance;

    public float x, y;

    public float gravityControl = 5f;

    private Rigidbody rb;
    private Animator anim;
    private Transform cam;

    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    [SerializeField] private TrailRenderer tr;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
        groundDistance = (GetComponent<CapsuleCollider>().height / 2) + 0.3f;
        tr.emitting = false;
        Physics.gravity *= gravityControl;

        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on player!");
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void Update()
    {
        IsGrounded();
        HandleJumpAndDash();
        

        x= Input.GetAxis("Horizontal");
        y= Input.GetAxis("Vertical");
        anim.SetFloat("VelX",x);
        anim.SetFloat("VelY",y);

        if (IsGrounded())
        {
            tr.emitting = false;
        }
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Utilizar MovePosition para actualizar la posición del Rigidbody
            rb.MovePosition(transform.position + moveDir * playerSpeed * Time.fixedDeltaTime);
        }
    }

    void HandleJumpAndDash()
    {
        if (Input.GetButtonDown("Jump"))
        {
            anim.SetBool("IsJumping", true);
            if (IsGrounded())
            {
                
                
                float requiredVelocity = Mathf.Sqrt(2 * jumpForce * Mathf.Abs(Physics.gravity.y));
                // Establecer la velocidad vertical directamente
                rb.velocity = new Vector3(rb.velocity.x, requiredVelocity, rb.velocity.z);

                canDash = true;
            }
             
            else if (!IsGrounded() && canDash)
            {
                
                StartCoroutine(Dash());
            }
        }
    }
    private IEnumerator Dash()
    {
        canDash = false;
        tr.emitting = true;

        rb.velocity = new Vector3(transform.forward.x * dashForce, 0f, transform.forward.z * dashForce);
        yield return new WaitForSeconds(0.3f);
        rb.velocity = Vector3.zero;
        tr.emitting = false;

    }
    private bool IsGrounded()
    {
        RaycastHit hit;
        return Physics.Raycast(transform.position, -transform.up, out hit, groundDistance);
        //anim.SetBool("IsJumping", false);
       
    }
}