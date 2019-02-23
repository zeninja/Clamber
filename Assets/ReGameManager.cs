using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReGameManager : MonoBehaviour {

	public Hand hand;

	void Start() {

	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.R) || Input.touchCount == 3) {
			Reset();
		}
	}

	void Reset() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
