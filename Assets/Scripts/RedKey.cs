using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedKey : MonoBehaviour {

	public GameObject[] barriers;
	public Sprite off;

	private bool triggered = false;

	void OnTriggerEnter2D(Collider2D other){
		if(!triggered){
			if(other.gameObject.tag == "Player"){
				Vector3 newPos = gameObject.transform.position;
				newPos.x += .3f;
				gameObject.transform.position = newPos;
				//gameObject.SetActive (false);
				gameObject.GetComponent<SpriteRenderer> ().sprite = off;
				foreach (GameObject o in barriers){
					o.SetActive (false);
				}
				triggered = true;
			}
		}
	}


}
