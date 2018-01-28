using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

	public float maxSpeed = 10;
	public float jumpSpeed = 10;

	/*Jump Heights: 5 is one block high; 7 is two blocks high*/

	public float height = 0.6f;

	//height of ridigbody

	public string thisLevel;
	public string nextLevel;

	private bool isGrounded = true;
	private bool isFacingRight = true;
	private Rigidbody2D rb2d;
	//private Animator anim;

	// Use this for initialization
	void Start() {
		rb2d = GetComponent<Rigidbody2D>();
		//anim = GetComponent<Animator>();
	}

	// FixedUpdate is called once per frame
	void Update() {
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
			//anim.Play("PlayerWalking");
		}
		else {
			//anim.Play("PlayerIdle");
		}

		rb2d.velocity = new Vector2(x * maxSpeed, rb2d.velocity.y);
		if (isGrounded && Input.GetButtonDown ("Jump")) {
			//isGrounded = false;
			rb2d.AddForce (Vector2.up * jumpSpeed, ForceMode2D.Impulse);
		} else if (Input.GetButtonUp ("Jump") && rb2d.velocity.y > 0.0f)
			rb2d.velocity = new Vector2 (rb2d.velocity.x, 0.0f);

		//attemp to change isGrounded to raycast
		RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
		if (hit.collider != null) {
			float distance = Mathf.Abs (hit.point.y - transform.position.y);
			if (distance < height / 2.0f + .01f)
				isGrounded = true;
			else
				isGrounded = false;
		}
	}

	private void OnCollisionEnter2D(Collision2D col) {
		if(this.enabled){
			switch (col.gameObject.tag) {
				/*
			case "Ground":
				isGrounded = true;
				break;
				*/
			case "Controllable":
			case "Projectile":
				//Debug.Log ("This: " + gameObject + "\nOther: " + col.gameObject);
				//SceneManager.LoadScene ("Level8");
				Die ();
				break;
			default:
				Debug.LogWarning ("There is no default behavior for player collisions with: " + col.gameObject);
				break;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D col){
		if(this.enabled){
			if(col.gameObject.tag == "Projectile"){
				//Debug.Log ("This: " + gameObject + "\nOther: " + col.gameObject);
				//SceneManager.LoadScene ("Level8");
				Die ();
			}

		}

	}


	void Flip() {
		// Switch the way the player is labelled as facing
		isFacingRight = !isFacingRight;

		// Multiply the player's x local scale by -1
		//Vector3 theScale = transform.localScale;
		//theScale.x *= -1;
		//transform.localScale = theScale;
	}

	public void Die(){
		UnityEngine.SceneManagement.SceneManager.LoadScene (thisLevel);
	}

	public void NextLevel(){
		UnityEngine.SceneManagement.SceneManager.LoadScene (nextLevel);
	}
}
