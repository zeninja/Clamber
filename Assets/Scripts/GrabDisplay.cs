using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabDisplay : MonoBehaviour {

	public ShapeMaker circle;
	public Transform followTarget;
	public float jumpRadius = 3, grabRadius = .6f, failRadius = 1.5f;
	public float grabDuration = .125f;

	void Update() {
		AdjustRadius();
	}

	void LateUpdate() {
		transform.position = followTarget.transform.position;
	}

	float targetRadius;

	void AdjustRadius() {
		switch(Hand.state) {
			case Hand.HandState.Jumping:
				targetRadius = jumpRadius;
				break;
			// case Hand.HandState.GrabSuccess:
			// 	targetRadius = grabRadius;
			// 	break;
			// case Hand.HandState.GrabFailed:
			// 	targetRadius = failRadius;
			// 	break;

		}

		if (circle != null)  {
			circle.radius = targetRadius;	
		}
	}

	public void HandleJump() {
		targetRadius = jumpRadius;
	}

	public void HandleGrab(bool success) {
		float r = success ? grabRadius : failRadius;
		StartCoroutine(ShowGrab(r));
	}

	IEnumerator ShowGrab(float endRadius) {
		float t = 0;
		float d = grabDuration;

		float startRadius = jumpRadius;
		float difference  = endRadius - startRadius;

		while(t < d) {
			float p = Mathf.Clamp01(t / d);
			t += Time.fixedDeltaTime;

			targetRadius = startRadius + difference * (EZEasings.SmoothStop5(p));
			yield return new WaitForFixedUpdate();
		}
	}

	public float GetOuterRadius() {
		return targetRadius;
	}
}
