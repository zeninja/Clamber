using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldOutline : MonoBehaviour {

	Hold hold;
	// List<LineRenderer> lines;

	// Use this for initialization
	void Start () {
		hold = GetComponentInParent<Hold>();
		tracedPositions = new List<Vector3>();
		tracedPositions = GetColliderPoints();
		GenerateLines(tracedPositions);
	}

	List<Vector3> tracedPositions;
	int NUM_POSITIONS = 300;

	List<Vector3> GetColliderPoints() {
		Vector2[]     pts   = hold.GetComponent<PolygonCollider2D>().points;
		List<Vector3> final = new List<Vector3>();

		for (int i = 0; i < pts.Length; i++) {
			final.Add(new Vector3(pts[i].x, pts[i].y, 0));
		}
		return final;
	}

	public float width;

	void GenerateLines(List<Vector3> colliderPts) {
		int numLines = colliderPts.Count;
		int ptsPerLine = NUM_POSITIONS / numLines;

		for (int i = 0; i < numLines; i++) {
			GameObject newLine = new GameObject();
			newLine.transform.parent = transform;
			newLine.transform.localPosition = Vector3.zero;

			LineRenderer line   = newLine.AddComponent<LineRenderer>();
			line.numCapVertices = 90;

			List<Vector3> linePts = new List<Vector3>();
			for (int x = 0; x < ptsPerLine; x++) {
				int nextIndex = (i+1) % colliderPts.Count;
				linePts.Add(Vector3.Lerp(colliderPts[i], colliderPts[ nextIndex ], (float)x / (float)ptsPerLine));
			}
			line.positionCount = ptsPerLine;
			line.SetPositions(linePts.ToArray());

			line.startWidth = width;
			line.endWidth   = width;

			line.useWorldSpace = false;
			
		}
	}
}
