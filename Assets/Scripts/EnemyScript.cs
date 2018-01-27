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

	private bool towardPt1 = true;
	private bool patrol = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 selfPosition = transform.position;
		Vector2 playerPosition = player.transform.position;

		RaycastHit2D hit = Physics2D.Raycast(selfPosition,
			playerPosition - selfPosition);

		if (hit.collider != null && hit.collider.gameObject.tag == "Player") {
			patrol = false;
			transform.position = Vector2.MoveTowards(transform.position,
				player.transform.position, enemySpeed);
			float enemyToPlayerDist =
				Mathf.Abs(Vector2.Distance(selfPosition, playerPosition));
			if (enemyToPlayerDist < detectDistance) {
				transform.position = Vector2.MoveTowards(transform.position,
					player.transform.position, enemySpeed);
			}
		}
		else{
			patrol = true;
		}

		if((Vector2)transform.position == patrolPt1 || (Vector2)transform.position == patrolPt2){
			towardPt1 = !towardPt1;
		}

		if(patrol){
			if(towardPt1){
				transform.position = Vector2.MoveTowards (transform.position, patrolPt1, enemySpeed);
			}
			else{
				transform.position = Vector2.MoveTowards (transform.position, patrolPt2, enemySpeed);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		Debug.Log ("Enemy Collided");
		if(col.gameObject.tag == "Controllable"){
			towardPt1 = !towardPt1;
			Debug.Log ("Bounce on Enemy");
		}
	}

}
