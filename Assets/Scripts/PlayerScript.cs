using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public float maxSpeed = 10;
	public float jumpSpeed = 10;

	/*Jump Heights: 5 is one block high; 7 is two blocks high*/

	private bool isGrounded = true;
	private bool isFacingRight = true;
	private Rigidbody2D rb2d;
	private Animator anim;

	// Use this for initialization
	void Start() {
		rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	// FixedUpdate is called once per frame
	void FixedUpdate() {
		float x = Input.GetAxis("Horizontal");

		if (x < 0f && isFacingRight) {
			//If we're moving right but not facing right, flip the sprite and set facingRight to true.
			Flip();
			isFacingRight = false;
		}
		else if (x > 0f && !isFacingRight) {
			Flip();
			isFacingRight = true;
		}

		if (x != 0) {
			anim.Play("PlayerWalking");
		}
		else {
			anim.Play("PlayerIdle");
		}

		rb2d.velocity = new Vector2(x * maxSpeed, rb2d.velocity.y);
		if (isGrounded && Input.GetButtonDown("Jump")) {
			isGrounded = false;
			rb2d.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		isGrounded = true;
	}

	void Flip() {
		// Switch the way the player is labelled as facing
		isFacingRight = !isFacingRight;

		// Multiply the player's x local scale by -1
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
