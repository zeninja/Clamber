using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StyleManager : MonoBehaviour {

	public static float LINE_WIDTH = .0125f;
	
	public List<Color> palette;


	public static List<Color> COLOR_PALETTE;

	void Start() {
		COLOR_PALETTE = palette;
	}

	void Update() {
		#if UNITY_EDITOR
		COLOR_PALETTE = palette;
		#endif
	}
}
