using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Similar to aoe attack: Attach to enemy, set public variables, then call tryToAttack
// and pass in the player object

public class projectileAttack : MonoBehaviour {
	
	public GameObject projPrefab;	// Prefab of the projectile
	public float projSpeed;			// How fast the projectiles will move (150 is pretty good)
	public float cooldown;			// How long until the projectile can be shot again
	public float projDestroTime;	// How long the projectile stays un-destroyed

	private GameObject instantiatedObject;
	private GameObject player; // Is set when tryToAttack is called
	private float attackTimer = 0.0f;
	private bool isAttacking = false;

	void Update() {
		attackTimer += Time.deltaTime;

		// For debug testing
		// if (Input.GetMouseButtonDown(0)) {
		// 	tryToAttack(player);
		// }
	}

	public void tryToAttack(GameObject player) {
		this.player = player;
		if (attackTimer >= cooldown && !isAttacking) {
			isAttacking = true;
			attackTimer = 0.0f;
			attack();
		}
	}

	void attack() {
		instantiatedObject = Instantiate(projPrefab, transform.position, Quaternion.identity);
		Vector3 forceVector = (player.transform.position - transform.position);
		Rigidbody2D shotRb = instantiatedObject.GetComponent<Rigidbody2D>();
		shotRb.AddForce (forceVector.normalized * projSpeed);

		instantiatedObject.GetComponent<ProjectileScript>().setKillTime(projDestroTime);

		isAttacking = false;
	}
}
