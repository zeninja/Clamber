using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UIManager : MonoBehaviour {

	bool showSettings;
	bool showStats;

	public bool lowUIMode;

	void Update() {
		showStats = showStats && !lowUIMode;
		showSettings = showSettings && !lowUIMode;;

		

	}

	public void InvertShowStats() {
		showStats = !showStats;
	}

	public void InvertShowSettings() {
		showSettings = !showSettings;
	}
}
