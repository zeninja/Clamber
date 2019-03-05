using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	bool showSettings;
	bool showStats;

	void Update() {
		// if() {

		// }
	}

	public void InvertShowStats() {
		showStats = !showStats;
	}

	public void InvertShowSettings() {
		showSettings = !showSettings;
	}
}
