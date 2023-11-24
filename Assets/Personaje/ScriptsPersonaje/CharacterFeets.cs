using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFeets : MonoBehaviour
{

    public CharacterMovements characterMovements;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other) {
        characterMovements.canJump = true;
    }

    private void OnTriggerExit(Collider other) {
        characterMovements.canJump = false;
    }
}
