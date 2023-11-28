using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFeets : MonoBehaviour
{

    public CharacterMovements characterMovements;

    private void OnTriggerStay(Collider other) {
        characterMovements.canJump = true;
    }

    private void OnTriggerExit(Collider other) {
        characterMovements.canJump = false;
    }
}
