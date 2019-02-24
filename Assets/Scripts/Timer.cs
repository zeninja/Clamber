using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

	public float startTime;
	float endTime;

	float elapsedTime;

	string timeText;

	void Update() {
		elapsedTime = Time.time - startTime;
		timeText = elapsedTime.ToString("F3");
	}

	void SetStart() {
		startTime = Time.time;
	}

	void SetEnd() {
		endTime = Time.time;

	}
}
