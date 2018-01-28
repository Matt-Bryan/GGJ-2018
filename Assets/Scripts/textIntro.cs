﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textIntro : MonoBehaviour {

	private float currentTime = 0.0f;
	public float maxTime;
	public float firstStep;
	public float secondStep;
	public float thirdStep;
	public float fourthStep;
	public float fifthStep;
	public float sixthStep;
	public float seventhStep;

	public AudioClip gate;

	private Text canvasText;
	private AudioSource soundSource;

	// Use this for initialization
	void Start () {
		soundSource = GetComponent<AudioSource> ();
		canvasText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		currentTime += Time.deltaTime;
		if (currentTime < firstStep) {
			canvasText.text = ("Humanity, in their hubris, sought to defeat the old gods");
		} else if (currentTime < secondStep) {
			canvasText.text = ("They soon realized that their only hope was to restrain the gods");
		} else if (currentTime < thirdStep) {
			canvasText.text = ("the god of deceit was lured into a cave beneath a great volcano...");
		} else if (currentTime < fourthStep) {
			canvasText.text = ("And they trapped you here");
		} else if (currentTime < fifthStep) {
			canvasText.text = ("But that was a long time ago");
		} else if (currentTime < sixthStep) {
			canvasText.text = ("Now something has returned to your prison");
		} else if (currentTime < seventhStep) {
			canvasText.text = ("And they come with new hosts");
		} else {
			canvasText.text = ("[Left Click] Take Control. Escape.");
			//GameObject.Find ("Arrow").SetActive (true);
		}
		
	}

	void Update() {
		if (Input.GetButtonDown("Fire1") && (currentTime > seventhStep) ){
			Destroy(canvasText.gameObject);
		}
	}
}
