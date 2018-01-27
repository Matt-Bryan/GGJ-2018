using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To use this script, attach to an actor, put in the public variables, 
// and then call tryToAttack()

public class aoeAttack : MonoBehaviour {

	public GameObject aoePrefab;	// Prefab of the aoe projectile
	public float projSpeed;			// How fast the projectiles will move (150 is pretty good)
	public float projDestroTime;	// How soon the projectile will be destroyed after firing
	public float cooldown;			// How long until the aoe can be shot again

	private GameObject[] instantiatedObjects = new GameObject[8];
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
			StartCoroutine(attack());
		}
	}

	IEnumerator attack() {
		for (int count = 0; count < 8; count++) {
			instantiatedObjects[count] = Instantiate(aoePrefab, transform.position, Quaternion.identity);
			Vector3 forceVector = new Vector3(Mathf.Cos(count * 15), Mathf.Sin(count * 15), 0);
			Rigidbody2D shotRb = instantiatedObjects[count].GetComponent<Rigidbody2D>();
			shotRb.AddForce (forceVector.normalized * projSpeed);
		}
		yield return new WaitForSeconds(projDestroTime);
		for (int count = 0; count < 8; count++) {
			Destroy(instantiatedObjects[count]);
		}
	}
}
