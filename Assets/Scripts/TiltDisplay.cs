using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltDisplay : MonoBehaviour {

	public GrabDisplay grab;																// MIGHT BE BETTER TO COMBINE THIS SCRIPT WITH THE GRAB DISPLAY??

	public Transform followTarget;
	public GameObject game_left, game_right, game_fwd;
	public GameObject dvc_left, dvc_right, dvc_fwd;

	public GameObject notch;

	void Start() {
		AdjustTiltDisplay();
	}

	void Update() {
		AdjustTiltDisplay();
	}

	void AdjustTiltDisplay() {
		Vector3 game_spreadL = new Vector3(0, 0, -LevelManager.HOLD_SPREAD ); 
		Vector3 game_spreadR = new Vector3(0, 0,  LevelManager.HOLD_SPREAD );
		Quaternion forward    = Hand.GetInstance().transform.rotation;

		game_left .GetComponent<RectTransform>().rotation = Quaternion.Euler(game_spreadL);
		game_right.GetComponent<RectTransform>().rotation = Quaternion.Euler(game_spreadR);
		game_fwd  .transform.rotation = forward;

		Vector3 dvc_spreadL   = new Vector3(0, 0, -InputManager.DEVICE_TILT_ANGLE ); 
		Vector3 dvc_spreadR   = new Vector3(0, 0,  InputManager.DEVICE_TILT_ANGLE );
		Vector3 dvc_fwdRot    = new Vector3(0, 0,  InputManager.inputHorizontal * InputManager.DEVICE_TILT_ANGLE );

		dvc_left .GetComponent<RectTransform>().rotation = Quaternion.Euler(dvc_spreadL);
		dvc_right.GetComponent<RectTransform>().rotation = Quaternion.Euler(dvc_spreadR);
		dvc_fwd  .GetComponent<RectTransform>().rotation = Quaternion.Euler(dvc_fwdRot);

		notch.transform.localPosition = new Vector3(0, grab.GetOuterRadius() / 2, 0);
	}

	void LateUpdate() {
		transform.position = followTarget.transform.position;
	}
}
