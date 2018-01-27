using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour {

	public float enemySpeed;
	public float detectDistance = 3f;
	public GameObject player;

	public Vector2 patrolPt1;
	public Vector2 patrolPt2;

	//private bool towardPt1 = true;
	private bool left = true;
	private bool patrol = true;

	/*An enemy can only be either projectile or aoe, not both.  If both scripts are set, 
	 * then projectiile takes precedence*/

	public projectileAttack projAtk;
	private bool isProjAtk = false;

	public aoeAttack aoe;
	private bool isAoe = false;


	// Use this for initialization
	void Start () {

		if(projAtk != null){
			isProjAtk = true;
		}
		else if(aoe != null){
			isAoe = true;
		}

	}
	
	// Update is called once per frame
	void Update () {
		Vector2 selfPosition = transform.position;
		Vector2 playerPosition = player.transform.position;

		RaycastHit2D hit = Physics2D.Raycast(selfPosition,
			playerPosition - selfPosition);

		if (hit.collider != null && hit.collider.gameObject.tag == "Player") {
			patrol = false;
			float enemyToPlayerDist =
				Mathf.Abs(Vector2.Distance(selfPosition, playerPosition));
			if (enemyToPlayerDist < detectDistance) {
				if(isProjAtk){
					projAtk.tryToAttack (hit.collider.gameObject);
				}
				else if(isAoe){
					//Debug.Log (aoe);
					aoe.tryToAttack ();
				}
				else{
				transform.position = Vector2.MoveTowards(transform.position,
					player.transform.position, enemySpeed);
				}
			}
		}
		else{
			patrol = true;
		}

//		if((Vector2)transform.position == patrolPt1 || (Vector2)transform.position == patrolPt2){
//			towardPt1 = !towardPt1;
//		}

		if(patrol){
			if(left){
				transform.position = Vector2.MoveTowards (transform.position, new Vector2(transform.position.x-1, transform.position.y), enemySpeed);
			}
			else{
				transform.position = Vector2.MoveTowards (transform.position, new Vector2(transform.position.x+1, transform.position.y), enemySpeed);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		//Debug.Log ("Enemy Collided");
		if(enabled == true && patrol && col.gameObject.tag == "Controllable"){
			left = !left;
			//Debug.Log ("Bounce on Enemy");
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if(enabled = true && patrol && col.gameObject.tag == "TurnPoint"){
			left = !left;
		}

	}

}
