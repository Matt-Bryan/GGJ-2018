using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduction : MonoBehaviour {

	public float camMoveDistance;
	public float camMoveTime;
	private float currentTime = 0;
	private Transform camTransform;
	// Use this for initialization
	void Start () {
		camTransform = transform.Find ("Main Camera").transform;
		camTransform.localPosition = new Vector3 (-60.0f, 0.0f, -10.0f);
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (currentTime < camMoveTime) {
			camTransform.position += new Vector3 (camMoveDistance, 0.0f, 0.0f);
			currentTime += Time.fixedDeltaTime;
		} else {
			camTransform.localPosition = new Vector3 (0.0f, 0.0f, -10.0f);
		}
			
	}
}
