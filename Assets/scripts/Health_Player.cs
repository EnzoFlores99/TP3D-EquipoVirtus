using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health_Player : MonoBehaviour
{
    public static Health_Player instance;
    private Rigidbody rb;
    public int vida = 5;
    public bool invencible = false;
    public float timepoInvencible = 1.5f;
    public float tiempoFrenado = 1.5f;
    private bool isInLava = false;

    public AudioSource audioGolpe; // referencia al audio de golpe

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //audioGolpe = GetComponent<AudioSource>();
    }

    public void RestarVida(int cantidad)
    {
        PlayPunchSound();

        if (!invencible && vida > 0)
        {
            vida -= cantidad;
            StartCoroutine(Invulnerabilidad());
            StartCoroutine(FrenarVelocidad());
            UIControllerHealt.instance.UpdateHealthDisplay();
        }
        if (vida <= 0)
        {
            CambiarEscena("Loser");
        }
    }

    public void CambiarEscena(string nombre)
    {

        SceneManager.LoadScene(nombre);

    }

    IEnumerator Invulnerabilidad()
    {
        invencible = true;
        yield return new WaitForSeconds(timepoInvencible);
        invencible = false;
    }

    IEnumerator FrenarVelocidad()
    {
        var velocidadActual = GetComponent<PlayerController>().playerSpeed;
        GetComponent<PlayerController>().playerSpeed /= 2;
        yield return new WaitForSeconds(tiempoFrenado);
        GetComponent<PlayerController>().playerSpeed = velocidadActual;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Traps") && !invencible)
        {
            RestarVida(1);
        }
        if (collision.gameObject.CompareTag("Lava") && !invencible)
        {
            if (!isInLava)
            {
                isInLava = true;
                RestarVida(1);

                if (rb != null)
                {
                    StartCoroutine(EmpujarJugador(collision));
                }
            }
        }
    }

    IEnumerator EmpujarJugador(Collision collision)
    {
        // Obtener la dirección relativa entre el jugador y la lava
        Vector3 pushDirection = (transform.position - collision.contacts[0].point).normalized;
        rb.AddForce(pushDirection * 50f, ForceMode.VelocityChange);
        yield return new WaitForSeconds(tiempoFrenado);
        isInLava = false;
    }

    private void PlayPunchSound()
    {

        if (audioGolpe != null)
        {
            audioGolpe.Play();
        }
    }
}
