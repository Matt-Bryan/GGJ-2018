using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour {

	public string nextLevel;

	void OnTriggerEnter2D(Collider2D other) {
		switch(other.gameObject.tag){
		case "Player":
			SceneManager.LoadScene (nextLevel);
			Debug.Log ("Next Level");
			break;
		case "Controllable":
			break;
		case "Arrow_Flying":
			break;
		case "Projectile":
			break;
		default:
			Debug.LogError ("Setup exit for this object! " + other.gameObject);
			break;
		}
	}

}
