using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spikes : MonoBehaviour {

	public string thisLevel;

	void OnTriggerEnter2D(Collider2D other) {
		switch(other.gameObject.tag){
		case "Player":
			//SceneManager.LoadScene (thisLevel);
			//Debug.Log ("Died");
			if(other.gameObject.GetComponent<PlayerScript> ().enabled){
				other.gameObject.transform.GetComponent<PlayerScript> ().Die ();	
			}
			break;
		case "Controllable":
			GameObject.Destroy (other.gameObject);
			break;
		case "Arrow_Flying":
			break;
		case "Projectile":
			break;
		default:
			Debug.LogError ("Setup spikes for this object! " + other.gameObject);
			break;
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			if(other.gameObject.GetComponent<PlayerScript> ().enabled){
				other.gameObject.transform.GetComponent<PlayerScript> ().Die ();	
			}
		}
		else if(other.gameObject.tag == "Enemy"){
			if(other.gameObject.GetComponent<EnemyScript> ().enabled){
				GameObject.Destroy (other.gameObject);
			}
		}
	}

}
