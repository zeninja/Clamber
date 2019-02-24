using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReGameManager : MonoBehaviour {

	private static ReGameManager instance;
	public static ReGameManager GetInstance() {
		return instance;
	}

	void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			if (this != instance) {
				Destroy(this.gameObject);
			}
		}
	}

	void Start() {
		
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.R) || Input.touchCount == 3) {
			Reset();
		}
	}

	public static void Reset() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
