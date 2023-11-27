using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObstacle : MonoBehaviour
{
   public int cantidad = 1;

   private void OnTriggerEnter (Collider other)
   {
        if (other.tag == "Player")
        {
            other.GetComponent<Health_Player>().RestarVida(cantidad);
        }
   }

   private void OnTriggerStay (Collider other)
   {
        if (other.tag == "Player")
        {
            other.GetComponent<Health_Player>().RestarVida(cantidad);
        }
   }
}
