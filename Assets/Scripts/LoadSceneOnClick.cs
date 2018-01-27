using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

	public void LoadLevel(string levelName) {
        if (levelName == "Quit") {
            Application.Quit();
        }
        else {
			UnityEngine.SceneManagement.SceneManager.LoadScene (levelName);
        }
    }
}
