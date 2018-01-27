using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spikes : MonoBehaviour {

	public string thisLevel;

	void OnTriggerEnter2D(Collider2D other) {
		SceneManager.LoadScene (thisLevel);
		Debug.Log ("Died");
	}

}
