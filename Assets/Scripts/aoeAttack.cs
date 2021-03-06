﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To use this script, attach to an actor, put in the public variables, 
// and then call tryToAttack()

public class aoeAttack : MonoBehaviour {

	public GameObject aoePrefab;	// Prefab of the aoe projectile
	public float projSpeed;			// How fast the projectiles will move (150 is pretty good)
	public float projDestroTime;	// How soon the projectile will be destroyed after firing
	public float cooldown;			// How long until the aoe can be shot again
	public int numProjectiles;      // The number of projectiles to be fired

	private List<GameObject> instantiatedObjects = new List<GameObject>(0);
	//private GameObject[] instantiatedObjects = new GameObject[8];
	private float attackTimer = 3.0f;

	void Update() {
		attackTimer += Time.deltaTime;

		// For debug testing
		// if (Input.GetMouseButtonDown(0)) {
		// 	tryToAttack();
		// }
	}

	public void tryToAttack() {
		if (attackTimer >= cooldown) {
			attackTimer = 0.0f;
			attack();
		}
	}

	void attack() {
		for (int count = 0; count < numProjectiles; count++) {
			instantiatedObjects.Add (Instantiate (aoePrefab, transform.position, Quaternion.identity));
			Vector3 forceVector = new Vector3(Mathf.Cos(count * 15), Mathf.Sin(count * 15), 0);
			Rigidbody2D shotRb = instantiatedObjects[instantiatedObjects.Count-1].GetComponent<Rigidbody2D>();
			shotRb.AddForce (forceVector.normalized * projSpeed);
			instantiatedObjects[count].GetComponent<ProjectileScript>().setKillTime(projDestroTime);
		}

		instantiatedObjects.Clear();
	}
}
