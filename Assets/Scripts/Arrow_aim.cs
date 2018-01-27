﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_aim : MonoBehaviour {
	private Transform arrowTransform;
	private Vector2 arrowDirection;
	private float mouseLocX;
	private float mouseLocY;
	public GameObject arrowProjectile;
	GameObject playerCamera;
	Quaternion rotation;
	private static float piTimes2 = 6.28f;
	// Use this for initialization
	void Start () {
		arrowTransform = GetComponent<Transform> ();
		playerCamera = GameObject.Find ("Main Camera");
		//arrowTransform.rotation.Set (0.0f, 0.0f, 90.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		mouseLocX = Input.mousePosition.x;
		mouseLocY = Input.mousePosition.y;
		arrowDirection = new Vector2 (mouseLocX - Screen.width / 2, mouseLocY - Screen.height / 2);
		rotation = Quaternion.Euler( 0, 0, (Mathf.Atan2 (arrowDirection.y, arrowDirection.x) / piTimes2 * 360) );
		arrowTransform.rotation = rotation;

		if (Input.GetButtonDown ("Fire1"))
			Shoot ();
		
		
	}

	void Shoot(){
		GameObject projectile = (GameObject)Instantiate (arrowProjectile, transform.position, transform.rotation);
		//projectile.GetComponent<Arrow_flight> ().SendMessage ("setPrevBody", transform.parent);
		projectile.GetComponent<Arrow_flight> ().prevBody = transform.parent.gameObject;
		playerCamera.transform.SetParent (projectile.transform);
		GetComponentInParent<PlayerScript> ().enabled = false;
		Destroy (arrowTransform.gameObject);
	}
}
