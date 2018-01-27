using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_flight : MonoBehaviour {
	private float flight_time = 3.0f;
	private bool is_returning = false;
	public GameObject prevBody;
	public int arrowSpeed;
	public GameObject arrowAiming;
	Rigidbody2D arrowRigidbody;

	// Use this for initialization
	void Start () {
		arrowRigidbody = GetComponent<Rigidbody2D> ();
		//arrowRigidbody.AddForce( transform.up * 10.0f, ForceMode2D.Impulse);
		is_returning = false;
		arrowRigidbody.velocity = transform.right * arrowSpeed;
		StartCoroutine ("ArrowTimedReturn");
	}
	
	// Update is called once per frame
	void Update () {
		
		if (is_returning)
			arrowRigidbody.velocity = (prevBody.transform.position - transform.position) * 3;

	}

	void OnTriggerEnter2D (Collider2D col){
		Debug.Log ("collision");
		if (col.gameObject.tag == "Controllable")
			takeControl (col.gameObject);
		else if (col.gameObject.tag == "Ground")
			arrowReturn ();
			
	}

	void takeControl(GameObject newBody){
		newBody.GetComponent<PlayerScript> ().enabled = true;
		newBody.GetComponent<EnemyScript> ().enabled = false;
		newBody.tag = "Player";
		GameObject.Instantiate (arrowAiming, newBody.transform);
		GameObject.Find ("Main Camera").transform.SetParent (newBody.transform);
		prevBody.tag = "Controllable";
		Destroy (gameObject);
	}

	public void setPrevBody(GameObject oldBody){
		prevBody = oldBody;
	}

	IEnumerator ArrowTimedReturn(){
		yield return new WaitForSeconds(flight_time);
		arrowReturn ();
	}
		
	void arrowReturn(){
		prevBody.tag = "Controllable";
		is_returning = true;
	}
}
