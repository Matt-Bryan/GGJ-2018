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
		is_returning = false;
		arrowRigidbody.velocity = transform.right * arrowSpeed;
		StartCoroutine ("ArrowTimedReturn");
	}
	
	// Update is called once per frame
	void Update () {
		
		if (is_returning && prevBody != null)
			arrowRigidbody.velocity = (prevBody.transform.position - transform.position) * 3;

	}

	void OnTriggerEnter2D (Collider2D col){
		if (col.gameObject.tag == "Controllable" && (!col.gameObject.Equals(prevBody) || is_returning) )
			takeControl (col.gameObject);
		else if (col.gameObject.tag == "Ground")
			arrowReturn ();		
	}

	void takeControl(GameObject newBody){
		prevBody.tag = "Controllable";
		newBody.GetComponent<PlayerScript> ().enabled = true;
		newBody.GetComponent<EnemyScript> ().enabled = false;
		newBody.tag = "Player";
		GameObject.Instantiate (arrowAiming, newBody.transform);
		GameObject.Find ("Main Camera").transform.SetParent (newBody.transform);
		Destroy (gameObject);
	}

	IEnumerator ArrowTimedReturn(){
		yield return new WaitForSeconds(flight_time);
		arrowReturn ();
	}

	void SetPrevBody(GameObject oldBody){
		prevBody = oldBody;
		prevBody.gameObject.tag = "Controllable";
	}
		
	void arrowReturn(){
		is_returning = true;
	}
}
