using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabDisplay : MonoBehaviour {


	public float holdRadius;
	public float jumpRadius;
	float targetRadius;
	float ringRadius;

	RingEffect ring;

	void Start() {
		ring = GetComponentInChildren<RingEffect>();
	}
	
	// Update is called once per frame
	void Update () {
		SetRadius();
		ApplyRadius();
	}



	public Transform followTarget;

	void LateUpdate() {
		transform.position = followTarget.transform.position;
	}

	void SetRadius() {

		if(Hand.OnHold()) {
			targetRadius = holdRadius;
		} else 
		if(Hand.Jumping()) {
			targetRadius = jumpRadius;
		}

		ringRadius = targetRadius; //Mathf.Lerp(circleRadius, targetRadius, Time.deltaTime);
	}

	void ApplyRadius() {
		// ring.radius = ringRadius;
	}
}
