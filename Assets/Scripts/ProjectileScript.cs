using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

	private float killTime;
	private float tiemr = 0.0f;

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Ground" || col.gameObject.tag == "Wall"){
			GameObject.Destroy (gameObject);
		}
	}

	public void setKillTime(float time) {
		killTime = time;
	}

}
