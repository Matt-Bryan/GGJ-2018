using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneWrangler : MonoBehaviour {

	public string thisLevel;
	public string nextLevel;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void Die(){
		UnityEngine.SceneManagement.SceneManager.LoadScene (thisLevel);
		Debug.Log ("you died");
	}

	public void Contitnue(){
		UnityEngine.SceneManagement.SceneManager.LoadScene (nextLevel);
	}

}
