using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalEnd : MonoBehaviour
{
    
    private void OnTriggerEnter (Collider other)
   {
        if (other.tag == "Player")
        {
             CambiarEscena("Winner");
        }
   }
   public void CambiarEscena(string nombre){

    SceneManager.LoadScene(nombre);

   }
}
