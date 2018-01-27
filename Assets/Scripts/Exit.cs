using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour {

	public string nextLevel = "TestingJohn1";

	void OnTriggerEnter2D(Collider2D other) {
		SceneManager.LoadScene (nextLevel);
		Debug.Log ("Next Level");
	}

}
