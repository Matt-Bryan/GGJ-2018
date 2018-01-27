using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_flight : MonoBehaviour {
	public GameObject prevBody = null;
	public int arrowSpeed;
	public GameObject arrowAiming;
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

	void OnTriggerEnter2D (Collider2D col){
		Debug.Log ("collision");
		if (col.gameObject.tag == "Controllable" && prevBody != null)
			takeControl (col.gameObject);
			
	}

	void takeControl(GameObject newBody){
		prevBody.tag = "Controllable";
		newBody.GetComponent<PlayerScript> ().enabled = true;
		newBody.tag = "Player";
		GameObject.Instantiate (arrowAiming, newBody.transform);
		GameObject.Find ("Main Camera").transform.SetParent (newBody.transform);
		Destroy (gameObject);
	}

	public void setPrevBody(GameObject oldBody){
		prevBody = oldBody;
	}
}
