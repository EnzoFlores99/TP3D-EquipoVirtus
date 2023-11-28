using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 12f;
    public float rotationSpeed = 200.0f;
    public float jumpForce = 10f;
    public AudioSource audioSource; // referencia al audio de salto
    public AudioSource audioDash; // referencia al audio del dash
    //private AudioSource audioGolpe; // referencia al audio de golpe

    //public GameObject sonidoGolpe;
    public float dashForce = 300f;
    private bool canDash = false;
    private float groundDistance;
    private Animator anim;
    public Vector3 gravityControl = new Vector3(0f, -98f, 0f);

    private Rigidbody rb;
    [SerializeField] private TrailRenderer tr;

    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        Vector3 scale = transform.localScale;
        groundDistance = scale.y + 0.3f;

        tr.emitting = false;
        Physics.gravity = gravityControl;

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
    }

    void MovePlayer()
    {
        float horizontal, vertical;
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        transform.Rotate(0, horizontal * Time.deltaTime * rotationSpeed, 0);
        transform.Translate(0, 0, vertical * Time.deltaTime * playerSpeed);

        anim.SetFloat("VelX", horizontal);
        anim.SetFloat("VelY", vertical);
        anim.SetBool("InFloor", true);
    }

    void HandleJumpAndDash()
    {
        if (Input.GetButtonDown("Jump"))
        {
            anim.SetBool("IsJumping", true);
            if (IsGrounded())
            {
                anim.SetBool("IsJumping", true);
                PlayJumpSound();

                float requiredVelocity = Mathf.Sqrt(2 * jumpForce * Mathf.Abs(Physics.gravity.y));

                // Establecer la velocidad vertical directamente
                rb.velocity = new Vector3(rb.velocity.x, requiredVelocity, rb.velocity.z);

                canDash = true;
            }
            else if (canDash)
            {

                StartCoroutine(Dash());
            }
        }
        if (!IsGrounded())
        {
            anim.SetBool("InFloor", false);
            anim.SetBool("IsJumping", false);
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        tr.emitting = true;
        PlayDashSound();

        // Almacena la gravedad original
        Vector3 originalGravity = Physics.gravity;

        // Obtén la dirección hacia adelante del personaje
        Vector3 dashDirection = transform.forward;

        // Aplica la fuerza de dash como una fuerza adicional
        rb.AddForce(dashDirection * dashForce, ForceMode.VelocityChange);

        // Reduce la gravedad temporalmente
        Physics.gravity /= gravityControl.y;

        yield return new WaitForSeconds(0.3f);

        // Restaura la gravedad original
        Physics.gravity = originalGravity;

        rb.velocity = Vector3.zero;
        tr.emitting = false;
    }

    private bool IsGrounded()
    {
        RaycastHit hit;
        return Physics.Raycast(transform.position, -transform.up, out hit, groundDistance);
        //anim.SetBool("IsJumping", false);

    }

    private void PlayJumpSound(){
        
        if (audioSource != null){
            audioSource.Play();
        }
    }

    private void PlayDashSound(){
        
        if (audioDash != null){
            audioDash.Play();
        }
    }
}