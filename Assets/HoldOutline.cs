using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldOutline : MonoBehaviour {

	Hold hold;
	LineRenderer line;

	// Use this for initialization
	void Start () {
		hold = GetComponentInParent<Hold>();
		line = GetComponent<LineRenderer>();
		tracedPositions = new List<Vector3>();
		tracedPositions = GetColliderPoints();

	}

	List<Vector3> tracedPositions;
	Vector3[] linePositions;

	int NUM_POSITIONS = 300;

	List<Vector3> GetColliderPoints() {
		Vector2[] pts   = hold.GetComponent<PolygonCollider2D>().points;
		Vector3[] ptsVec3 = new Vector3[pts.Length];

		for (int i = 0; i < pts.Length; i++) {
			ptsVec3[i] = new Vector3(pts[i].x, pts[i].y, 0);
		}

		List<Vector3> final = new List<Vector3>();

		// for(int x = 0; x < pts.Length; x++) {
		// 	for(int i = 0; i < NUM_POSITIONS / pts.Length; i++) {
				
		// 		Vector3 pos1 = ptsVec3[x];
		// 		Vector3 pos2 = ptsVec3[(x + 1) % pts.Length];

		// 		final[i] = Vector3.Lerp(pos1, pos2, (float)i / (float)NUM_POSITIONS);
		// 	}
		// }


		return final;
	}

	void Update() {
		// SetLinePositions();
	}

	void SetLinePositions() {
		int length 	  = Mathf.CeilToInt(hold.elapsedPct * tracedPositions.Count);
		linePositions = tracedPositions.GetRange(0, length).ToArray();

		line.SetPositions(linePositions);
	}
}
