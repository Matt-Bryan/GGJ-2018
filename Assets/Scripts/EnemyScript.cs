using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	public float enemySpeed;
	public float detectDistance = 1f;
	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 selfPosition = transform.position;
		Vector2 playerPosition = player.transform.position;

		RaycastHit2D hit = Physics2D.Raycast(selfPosition, playerPosition - selfPosition);

		if (hit.collider != null && hit.collider.gameObject.tag == "Player") {
			transform.position = Vector2.MoveTowards(transform.position,
				player.transform.position, enemySpeed);
		}
	}
}
