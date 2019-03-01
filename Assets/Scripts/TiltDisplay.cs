using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltDisplay : MonoBehaviour {

	public GameObject left, right;
	public Transform followTarget;

	void Update() {

		Vector3 leftTilt  = new Vector3(0, 0, -HoldSpawner.HOLD_SPREAD ); 
		Vector3 rightTilt = new Vector3(0, 0,  HoldSpawner.HOLD_SPREAD );

		left .GetComponent<RectTransform>().rotation = Quaternion.Euler(leftTilt);
		right.GetComponent<RectTransform>().rotation = Quaternion.Euler(rightTilt);
	}

	void LateUpdate() {
		transform.position = followTarget.transform.position;
	}
}
