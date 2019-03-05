using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltDisplay : MonoBehaviour {
	public Transform followTarget;
	public GameObject left, right, fwd;


	// GameObject[] children;

	void Start() {
		AdjustTiltDisplay();
	}

	void Update() {
		AdjustTiltDisplay();
	}

	void AdjustTiltDisplay() {
		Vector3 leftTilt  = new Vector3(0, 0, -LevelManager.HOLD_SPREAD ); 
		Vector3 rightTilt = new Vector3(0, 0,  LevelManager.HOLD_SPREAD );
		Quaternion forward   = Hand.GetInstance().transform.rotation;

		left .GetComponent<RectTransform>().rotation = Quaternion.Euler(leftTilt);
		right.GetComponent<RectTransform>().rotation = Quaternion.Euler(rightTilt);
		fwd  .transform.rotation = forward;

		// left .SetActive(Hand.OnHold());
		// right.SetActive(Hand.OnHold());
	}

	void LateUpdate() {
		transform.position = followTarget.transform.position;
	}
}
