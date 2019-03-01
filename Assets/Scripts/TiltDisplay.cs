using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltDisplay : MonoBehaviour {

	// public GameObject left, right;
	Vector3 leftTilt, rightTilt;

	void Update() {

		if(ReHand.currentRotation.z < InputManager.MAX_TILT_ANGLE) {
			Vector3 rightTilt = new Vector3(0, 0,  InputManager.MAX_TILT_ANGLE - ReHand.currentRotation.z + 180);
			Vector3 leftTilt  = new Vector3(0, 0, -InputManager.MAX_TILT_ANGLE - ReHand.currentRotation.z + 180);
		} else if (ReHand.currentRotation.z > 360 - InputManager.MAX_TILT_ANGLE) {
			Vector3 rightTilt = new Vector3(0, 0,  InputManager.MAX_TILT_ANGLE + ReHand.currentRotation.z - 180);
			Vector3 leftTilt  = new Vector3(0, 0, -InputManager.MAX_TILT_ANGLE + ReHand.currentRotation.z - 180);
		}

		// GetComponent<RectTransform>().eulerAngles = ReHand.currentRotation * -1;

		// left .GetComponent<RectTransform>().rotation = Quaternion.Euler(leftTilt);
		// right.GetComponent<RectTransform>().rotation = Quaternion.Euler(rightTilt);
	}
}
