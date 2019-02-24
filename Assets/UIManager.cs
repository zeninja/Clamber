using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Button rotationButton;

	public static bool rotationTracking;

	public void SwitchRotationTracking() {
		rotationTracking = !rotationTracking;
		string rotString = "TRACK ROTATION: ";
		rotString += rotationTracking ? "ON" : "OFF";

		rotationButton.GetComponentInChildren<Text>().text = rotString;

	}
}
