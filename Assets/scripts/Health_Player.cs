using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health_Player : MonoBehaviour
{
     public static Health_Player instance;
    public int vida = 5;
    public bool invencible = false;
    public float timepoInvencible = 1.5f;
    public float tiempoFrenado = 1.5f;

    private void Awake()
    {
        instance = this;
    }

    public void RestarVida(int cantidad)
    {
        if (!invencible && vida > 0)
        {
            vida -= cantidad;
            StartCoroutine(Invulnerabilidad());
            StartCoroutine(FrenarVelocidad());
           UIControllerHealt.instance.UpdateHealthDisplay();
        }
        if (vida <= 0)
        {
            //gameObject.SetActive(false);
            //Debug.Log("GAMEOVER");
            CambiarEscena("Loser");
        }
       
    }
    public void CambiarEscena(string nombre){

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
        var velocidadActual = GetComponent<CharacterMovements>().velocidadMovimiento;
        GetComponent<CharacterMovements>().velocidadMovimiento = 2;
        yield return new WaitForSeconds(tiempoFrenado);
        GetComponent<CharacterMovements>().velocidadMovimiento= velocidadActual;
    }
}
