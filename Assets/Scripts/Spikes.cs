using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spikes : MonoBehaviour {

	public string thisLevel;

	void OnTriggerEnter2D(Collider2D other) {
		switch(other.gameObject.tag){
		case "Player":
			SceneManager.LoadScene (thisLevel);
			Debug.Log ("Died");
			break;
		case "Enemy":
			GameObject.Destroy (other.gameObject);
			break;
		default:
			Debug.LogError ("Setup spikes for this object! " + other.gameObject);
			break;
		}
	}

}
