// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// [RequireComponent(typeof(Rigidbody))]
// public class PlayerController : MonoBehaviour
// {
//     Rigidbody rb;
//     public float speed = 5f;
//     public float gravity = 9.8f;
//     public Transform cam;
//     private float turnSmoothTime = 0.1f;
//     private float turnSmoothVelocity;
//     public float jumpForce = 10f;
//     public LayerMask groundLayer;
//     private bool canDash = true;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody>();

//         if (rb == null)
//         {
//             Debug.Log("Rigidbody is missing");
//         }
//     }

//     void Update()
//     {
//         MoveRigidbody();
//         Jump();
//     }

//     void MoveRigidbody()
//     {
//         float horizontal = Input.GetAxisRaw("Horizontal");
//         float vertical = Input.GetAxisRaw("Vertical");
//         Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

//         if (direction.magnitude >= 0.1f)
//         {
//             float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
//             float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

//             Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

//             transform.rotation = Quaternion.Euler(0f, angle, 0f);

//             rb.velocity = new Vector3(moveDir.x * speed, rb.velocity.y, moveDir.z * speed);
//         }
//     }

//     void Jump()
//     {
//         if (Input.GetButtonDown("Jump") && IsGrounded())
//         {
//             // Aplicar impulso vertical para simular un salto
//             rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
//             canDash = true;
//         }
//     }

//     bool IsGrounded()
//     {
//         // Utilizar un pequeño raycast para verificar si está en el suelo
//         return Physics.Raycast(transform.position, Vector3.down, 0.1f, groundLayer);
//     }
// }