using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_flight : MonoBehaviour {

	public int arrowSpeed;
	Rigidbody2D arrowRigidbody;
	// Use this for initialization
	void Start () {
		arrowRigidbody = GetComponent<Rigidbody2D> ();
		//arrowRigidbody.AddForce( transform.up * 10.0f, ForceMode2D.Impulse);
		
	}
	
	// Update is called once per frame
	void Update () {
		arrowRigidbody.velocity = transform.right * arrowSpeed;
	}
}
