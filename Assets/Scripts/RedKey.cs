using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedKey : MonoBehaviour {

	public GameObject[] barriers;

	void OnTriggerEnter2D(Collider2D other){
		gameObject.SetActive (false);
		foreach (GameObject o in barriers){
			o.SetActive (false);
		}
	}


}
