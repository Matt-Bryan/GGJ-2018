using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Arrow_flight : MonoBehaviour {
	private float flight_time = 3.0f;
	private float flight_death_time = 5.0f;
	private bool is_returning = false;
	public GameObject prevBody;
	public int arrowSpeed;
	public GameObject arrowAiming;
	Rigidbody2D arrowRigidbody;
	private GameObject playerCamera;

	private Animator anim;

	// Use this for initialization
	void Start () {
		playerCamera = GameObject.Find("Main Camera");
		arrowRigidbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator>();

		is_returning = false;
		arrowRigidbody.velocity = transform.right * arrowSpeed;
		StartCoroutine ("ArrowTimedReturn");
	}
	
	// Update is called once per frame
	void Update () {
		anim.Play("SoulFire");
		if (is_returning && prevBody != null)
			arrowRigidbody.velocity = (prevBody.transform.position - transform.position) * 3;
	}

	void OnTriggerEnter2D (Collider2D col){
		if (col.gameObject.tag == "Controllable" && (is_returning || !col.gameObject.Equals (prevBody))) {
			takeControl (col.gameObject);
			prevBody.GetComponent<EnemyScript> ().gPlayer.player = col.gameObject;
		} else if (col.gameObject.tag == "Ground") {
			arrowReturn ();		
		}
	}

	void OnTriggerStay2D(Collider2D col){
		if ((col.gameObject == prevBody) && is_returning)
			takeControl (col.gameObject);
	}

	void takeControl(GameObject newBody){
		Debug.Log (is_returning);
		Debug.Log ("taking control");
		newBody.GetComponent<PlayerScript> ().enabled = true;
		prevBody.tag = "Controllable";
		prevBody.GetComponent<EnemyScript> ().enabled = true;
		newBody.tag = "Player";
		GameObject.Instantiate (arrowAiming, newBody.transform);
		playerCamera.transform.SetParent (newBody.transform);
		playerCamera.transform.localPosition = new Vector3 (0.0f, 0.0f, -10.0f);
		newBody.GetComponent<EnemyScript> ().enabled = false;
		Destroy (gameObject);
	}

	IEnumerator ArrowTimedReturn(){
		yield return new WaitForSeconds(flight_time);
		arrowReturn ();
		yield return new WaitForSeconds (flight_death_time);
		GameObject.Find ("GameManager").SendMessage ("Die");
	}
		
	void arrowReturn(){
		Debug.Log ("is returning");
		is_returning = true;
	}
}
