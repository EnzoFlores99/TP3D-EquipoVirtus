using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerHealt : MonoBehaviour
{
    public static UIControllerHealt instance;
    public Image heart1, heart2, heart3;
    public Sprite heartFull, heartEmpty;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void UpdateHealthDisplay()
    {
        // switch(GetComponent<Health_Player>().vida)
        switch(Health_Player.instance.vida)
        {
            case 3:
            heart1.sprite = heartFull;
            heart2.sprite = heartFull;
            heart3.sprite = heartFull;
            break;

            case 2:
            heart1.sprite = heartFull;
            heart2.sprite = heartFull;
            heart3.sprite = heartEmpty;
            break;

            case 1:
            heart1.sprite = heartFull;
            heart2.sprite = heartEmpty;
            heart3.sprite = heartEmpty;
            break;

            case 0:
            heart1.sprite = heartEmpty;
            heart2.sprite = heartEmpty;
            heart3.sprite = heartEmpty;
            break;
        }
    }
}
