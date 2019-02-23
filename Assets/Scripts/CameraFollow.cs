using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;

	public bool trackX, trackY;
	public float zDist = -10;

	public bool trackRotation;
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 camPos = Vector3.zero;

		float x = 0;
		float y = 0;

		if (trackX) {
			x = target.position.x;
		}

		if (trackY) {
			y = target.position.y;
		}

		transform.position = new Vector3(x, y, zDist);


	}
}
