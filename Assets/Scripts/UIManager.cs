using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UIManager : MonoBehaviour {

	bool SHOW_SETTINGS, SHOW_STATS;
	public bool showStats, showSettings;
	public bool lowUIMode;
	public bool settingsInteractable;

	public GameObject statsDisplay, settingsDisplay;

	void Update() {
		SHOW_STATS    = showStats	 && !lowUIMode;
		SHOW_SETTINGS = showSettings && !lowUIMode;

		statsDisplay   .SetActive(SHOW_STATS);
		settingsDisplay.SetActive(SHOW_SETTINGS);

		
	}

	public void InvertShowStats() {
		showStats = !showStats;
	}

	public void InvertShowSettings() {
		showSettings = !showSettings;
	}
}
