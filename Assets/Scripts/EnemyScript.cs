using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour {

	public float enemySpeed;
	public float detectDistance = 3f;

	private bool left = true;
	private bool patrol = true;

	/*
	 * An enemy can only be either projectile or aoe, not both.  If both scripts are set, 
	 * then projectiile takes precedence
	 */

	public projectileAttack projAtk;
	private bool isProjAtk = false;

	public aoeAttack aoe;
	private bool isAoe = false;

	private Animator enemyAnim;

	public GlobalPlayer gPlayer;

	// Use this for initialization
	void Start () {

		enemyAnim = GetComponent<Animator>();
		if (projAtk != null){
			isProjAtk = true;
		}
		else if(aoe != null){
			isAoe = true;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector2 selfPosition = transform.position;
		Vector2 playerPosition = gPlayer.player.transform.position;

		RaycastHit2D hit = Physics2D.Raycast(selfPosition,
			playerPosition - selfPosition);
		Debug.DrawRay (selfPosition, playerPosition - selfPosition);

		enemyAnim.Play("EnemyWalking");

		if (hit.collider != null && hit.collider.gameObject.tag == "Player") {
			float enemyToPlayerDist =
				Mathf.Abs(Vector2.Distance(selfPosition, playerPosition));
			if (enemyToPlayerDist < detectDistance) {
				patrol = false;
				if (isProjAtk) {
					projAtk.tryToAttack (hit.collider.gameObject);
				}
				else if (isAoe) {
					aoe.tryToAttack();
				}
				else {
					transform.position = Vector2.MoveTowards(transform.position,
						gPlayer.player.transform.position, enemySpeed);
					if ((selfPosition.x + playerPosition.x) < 0 &&
						transform.localScale.x > 0) {
						Flip();
					}
					else if ((selfPosition.x + playerPosition.x) > 0 &&
						transform.localScale.x < 0) {
						Flip();
					}
				}
			}
			else {
				patrol = true;
			}
		}
		else {
			patrol = true;
		}

		if (patrol) {
			if (left) {
				transform.position = Vector2.MoveTowards(transform.position,
					new Vector2(transform.position.x-1, transform.position.y), enemySpeed);
				if (transform.localScale.x > 0) {
					Flip();
				}
			}
			else {
				transform.position = Vector2.MoveTowards (transform.position,
					new Vector2(transform.position.x+1, transform.position.y), enemySpeed);
				if (transform.localScale.x < 0) {
					Flip();
				}
			}
		}
	}

	void Flip() {
		// Multiply the player's x local scale by -1
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (enabled == true && patrol && col.gameObject.tag == "Controllable") {
			left = !left;
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if(enabled == true && patrol && col.gameObject.tag == "TurnPoint"){
			left = !left;
		}
	}
}
