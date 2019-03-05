using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AspectRatioController : MonoBehaviour {

	// public UISlider slider;
	UnityEngine.UI.CanvasScaler scaler;

	public float landscapeAspectRatio;
	public float portraitAspectRatio;

	// Use this for initialization
	void Start () {
		scaler = GetComponent<CanvasScaler>();
	}
	
	// Update is called once per frame
	void Update () {
		landscapeAspectRatio = Camera.main.aspect;
		portraitAspectRatio  = 1 / landscapeAspectRatio;

		UpdateAspectRatios();
	}

	[Range(0, 1)]
	public float pct_tall = 0.5f;
	public float pct_16x9 = 0.5f;
	public float pct_wide = 0.5f;

	void UpdateAspectRatios() {
		if(portraitAspectRatio > 2) {
			// 19.5:9 (iPhone X and other very tall screens)
			scaler.matchWidthOrHeight = pct_tall;
		} else {
			// Everything else
			if (portraitAspectRatio == 1.777f) {
				scaler.matchWidthOrHeight = pct_16x9;
			} else {
				scaler.matchWidthOrHeight = pct_wide;
			}
		}
	}
}
