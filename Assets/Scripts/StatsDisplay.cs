using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsDisplay : MonoBehaviour {

	string statsText;
	public TextMeshProUGUI tmp_labels;
	public TextMeshProUGUI tmp_values;

	void Start() {

		string statsLabel = "\n" + 
		"gyro:" + "\n" +
		"roll:" + "\n" +
		"hand:" + "\n" +
		"time:" + "\n";

		tmp_labels.text = statsLabel;
	}
	
	// Update is called once per frame
	void Update () {
		DisplayStats();
	}

	void DisplayStats() {
		statsText = "\n" + 	// Skip first line because it's the header
							GyroToUnity(Input.gyro.attitude).eulerAngles + "\n" + 
							InputManager.roll.ToString("F2")			 + "\n" +
							InputManager.inputHorizontal.ToString("F2")	 + "\n" +
							Hand.currentRotation.z.ToString("F2")		 + "\n" +
							GameManager.burnDuration.ToString("F3");	

		tmp_values.text = statsText;
	}

	    Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
