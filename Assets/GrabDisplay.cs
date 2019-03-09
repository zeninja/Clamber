using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabDisplay : MonoBehaviour {

	public CircleEffect circle;
	public Transform followTarget;
	public float jumpRadius = 3, grabRadius = .6f;
	float targetRadius;
	public float grabDuration = .125f;

	void Start() {

	}

	void Update() {
		AdjustRadius();
	}

	void LateUpdate() {
		transform.position = followTarget.transform.position;
	}

	void AdjustRadius() {
		circle.radius = targetRadius;
	}

	public void HandleJump() {
		targetRadius = jumpRadius;
	}

	public void HandleGrab() {
		// StopCoroutine(ShowGrab());
		StartCoroutine(ShowGrab());
	}

	IEnumerator ShowGrab() {
		float t = 0;
		float d = grabDuration;

		while(t < d) {
			float p = Mathf.Clamp01(t / d);
			t += Time.fixedDeltaTime;

			targetRadius = grabRadius + (jumpRadius - grabRadius) *  (1 - EZEasings.SmoothStop3(p));

			yield return new WaitForFixedUpdate();
		}
	}

	public void HandleGrabFailed() {
		// do nothing right now...
	}
}
