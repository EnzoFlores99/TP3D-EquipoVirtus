using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
	[SerializeField] private float speed;
	public float limit = 750f;
	public bool randomStart = true;
	private float random = 0f;

	
	void Awake()
    {
		if(randomStart){
			random = Random.Range(0f, 3f);
		}
			
	}

    
    void Update()
    {
		float angle = limit * Mathf.Sin(Time.time + random * speed);
		transform.localRotation = Quaternion.Euler(0, 0, angle);
	}
}
