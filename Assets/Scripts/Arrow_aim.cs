﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_aim : MonoBehaviour {

	private SpriteRenderer sr;
	private PlayerScript parentPS;
	private Transform arrowTransform;
	private Vector2 arrowDirection;
	private float mouseLocX;
	private Rigidbody2D parentRB;
	private float mouseLocY;
	public GameObject arrowProjectile;
	GameObject playerCamera;
	Quaternion rotation;
	private static float piTimes2 = 6.28f;
	// Use this for initialization
	void Start () {
		parentPS = GetComponentInParent<PlayerScript> ();
		sr = GetComponent<SpriteRenderer> ();
		parentRB = GetComponentInParent<Rigidbody2D> ();
		arrowTransform = GetComponent<Transform> ();
		playerCamera = GameObject.Find ("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
		mouseLocX = Input.mousePosition.x;
		mouseLocY = Input.mousePosition.y;
		arrowDirection = new Vector2 (mouseLocX - Screen.width / 2, mouseLocY - Screen.height / 2);
		rotation = Quaternion.Euler( 0, 0, (Mathf.Atan2 (arrowDirection.y, arrowDirection.x) / piTimes2 * 360) );
		arrowTransform.rotation = rotation;

		if (Input.GetButtonDown ("Fire1")) {
			sr.enabled = true;
		} else if (Input.GetButtonUp ("Fire1")) {
			sr.enabled = true;
			Shoot ();
		}
	}

	void Shoot(){
		GameObject projectile = (GameObject)Instantiate (arrowProjectile, transform.position, transform.rotation);
		projectile.GetComponent<Arrow_flight> ().prevBody = transform.parent.gameObject;
		playerCamera.transform.SetParent (projectile.transform);
		if (parentPS != null) {
			parentPS.enabled = false;
			parentPS.playerAnim.Play ("PlayerIdle"); 
		}
		parentRB.velocity = new Vector2 (0.0f, parentRB.velocity.y);
		Destroy (arrowTransform.gameObject);
	}
}
