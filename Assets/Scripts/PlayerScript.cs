using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public float maxSpeed = 10;
	public float jumpSpeed = 10;

	/*Jump Heights: 5 is one block high; 7 is two blocks high*/

	private bool isGrounded = true;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start() {
		rb2d = GetComponent<Rigidbody2D>();
	}

	// FixedUpdate is called once per frame
	void FixedUpdate() {
		float x = Input.GetAxis("Horizontal");

		rb2d.velocity = new Vector2(x * maxSpeed, rb2d.velocity.y);
		if (isGrounded && Input.GetButtonDown("Jump")) {
			isGrounded = false;
			rb2d.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		isGrounded = true;
	}
}
