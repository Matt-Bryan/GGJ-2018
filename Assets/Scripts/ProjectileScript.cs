using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

	private float killTime = 100.0f;
	private float timer = 0.0f;

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Ground" || col.gameObject.tag == "Wall"){
			GameObject.Destroy (gameObject);
		}
	}

	void Update() {
		timer += Time.deltaTime;
		Debug.Log(timer);
		if (timer >= killTime) {
			Destroy(this.gameObject);
		}
	}

	public void setKillTime(float time) {
		killTime = time;
	}

}
