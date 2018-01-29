using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour {

	void Update(){
		if(Input.GetKey ("Escape")){
			Application.Quit ();
		}
	}

	public void Quit(){
		Application.Quit ();
	}

}
