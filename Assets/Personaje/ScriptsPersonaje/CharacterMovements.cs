using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovements : MonoBehaviour
{
    public float velocidadMovimiento = 5.0f;
    public float velocidadRotacion = 200.0f;
    private Animator anim;

    // variable sobre la posicion x e y del jugador
    public float x, y;

    public Rigidbody rb;
    public float salto = 8f;

    //variable booleana indecando si se puede saltar o no
    public bool canJump;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        canJump = false;
    }

    void FixedUpdate() 
    {
        transform.Rotate(0, x * Time.deltaTime*velocidadRotacion, 0);
        transform.Translate(0, 0, y * Time.deltaTime*velocidadMovimiento);
        
    }

    // Update is called once per frame
    void Update()
    {
        x= Input.GetAxis("Horizontal");
        y= Input.GetAxis("Vertical");


        anim.SetFloat("VelX",x);
        anim.SetFloat("VelY",y);

        if (canJump == true)
        {
          if(Input.GetKeyDown(KeyCode.Space))
          {
            anim.SetBool("IsJumping", true);
            rb.AddForce(new Vector3(0,salto,0), ForceMode.Impulse);

          }  
          anim.SetBool("InFloor", true); 
        }else
        {
            Falling();
        }
    }

    public void Falling()
    {
        anim.SetBool("InFloor", false);
        anim.SetBool("IsJumping", false);
    }
}
