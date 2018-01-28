using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedKey : MonoBehaviour {

	public GameObject[] barriers;
	public Sprite off;

	private AudioSource soundSource;

	private bool triggered = false;

	void OnTriggerEnter2D(Collider2D other){
		if(!triggered){
			if(other.gameObject.tag == "Player"){
				soundSource = GetComponent<AudioSource> ();
				soundSource.Play ();
				Vector3 newPos = gameObject.transform.position;
				newPos.x += .3f;
				gameObject.transform.position = newPos;
				//gameObject.SetActive (false);
				gameObject.GetComponent<SpriteRenderer> ().sprite = off;
				StartCoroutine ("DestroyGates");
				triggered = true;
			}
		}
	}

	//play audio cue from gates then destroy them
	IEnumerator DestroyGates (){
		yield return new WaitForSeconds (1.0f);
		foreach (GameObject o in barriers){
			o.GetComponent<AudioSource> ().Play ();
		}
		yield return new WaitForSeconds (1.0f);
		foreach (GameObject o in barriers){
			o.GetComponent<Animator>().Play("Open");
			o.GetComponent<Collider2D>().enabled = false;
		}
	}

}
