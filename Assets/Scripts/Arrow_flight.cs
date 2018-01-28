using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Arrow_flight : MonoBehaviour {
	
	public float flight_time = 1.50f;
	private float flight_death_time = 5.0f;
	public int arrowSpeed;

	private bool is_returning = false;
	private EnemyScript prevES;

	public AudioClip onCreate;
	public AudioClip whileFlying;
	public AudioClip onFail;
	//length of time the onCreate sound effect should play
	public float onCreateLength;
	private AudioSource soundSource;

	public GameObject prevBody;
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

		//plays launching sound on projectile creation
		soundSource = GetComponent<AudioSource> ();
		soundSource.clip = onCreate;
		soundSource.Play ();
		//sets timer to change from intitial sound to looped sound
		StartCoroutine("SoundChange");

		prevES = prevBody.GetComponent<EnemyScript> ();
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
		newBody.GetComponent<PlayerScript> ().CheckDirection ();
		newBody.GetComponent<PlayerScript> ().enabled = true;
		prevBody.tag = "Controllable";
		if (prevES != null) {
			prevES.enabled = false;
		}
		//prevBody.GetComponent<EnemyScript> ().enabled = true;

		prevBody.GetComponent <EnemyScript> ().CheckDirection ();
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
		//yield return new WaitForSeconds (flight_death_time);
		//GameObject.Find ("GameControllerObject").SendMessage ("Die");
	}

		
	IEnumerator SoundChange(){
		yield return new WaitForSeconds (onCreateLength);
		if (!is_returning) {
			soundSource.loop = true;
			soundSource.clip = whileFlying;
			soundSource.Play ();
		}
	}

	void arrowReturn(){
		is_returning = true;
		soundSource.loop = true;
		soundSource.clip = onFail;
		soundSource.Play ();
	}
}
