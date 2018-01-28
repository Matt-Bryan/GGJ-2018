﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Ground" || col.gameObject.tag == "Wall"){
			GameObject.Destroy (gameObject);
		}
	}

}
