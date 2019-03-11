using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldOutline : MonoBehaviour
{

    Hold hold;
    List<LineRenderer> lines;

    // Use this for initialization
    void Start()
    {
        hold = GetComponentInParent<Hold>();
        tracedPositions = new List<Vector3>();
        tracedPositions = GetColliderPoints();
        GenerateLines(tracedPositions);
    }

    void Update()
    {
        if (GetComponentInParent<Hold>().elapsedPct < 1 && updateLine)
        {
            DrawOutline();
        }
    }

	public bool updateLine = false;

    List<Vector3> tracedPositions;
    int NUM_POSITIONS = 300;

    List<Vector3> GetColliderPoints()
    {
        Vector2[] pts = hold.GetComponent<PolygonCollider2D>().points;
        List<Vector3> final = new List<Vector3>();

        for (int i = 0; i < pts.Length; i++)
        {
            final.Add(new Vector3(pts[i].x, pts[i].y, 0));
        }
        return final;
    }

    public float width;
	LineRenderer line;
    void GenerateLines(List<Vector3> colliderPts)
    {
        int ptsPerLine = NUM_POSITIONS / colliderPts.Count;

        line = gameObject.AddComponent<LineRenderer>();
		line.numCapVertices = 90;

		completeList = new List<Vector3>();

        for (int i = 0; i < colliderPts.Count; i++)
        {
            for (int x = 0; x < ptsPerLine; x++)
            {
                int nextIndex = (i + 1) % colliderPts.Count;
                completeList.Add(Vector3.Lerp(colliderPts[i], colliderPts[nextIndex], (float)x / (float)ptsPerLine));
            }
            line.positionCount = ptsPerLine * colliderPts.Count;
            line.SetPositions(completeList.ToArray());

            line.startWidth = width;
            line.endWidth = width;

            line.useWorldSpace = false;

        }
    }

    List<Vector3> completeList;

    void DrawOutline()
    {
        int shortRange = (int)(completeList.Count * (1 - hold.elapsedPct));
        List<Vector3> shortenedList = completeList.GetRange(0, shortRange);
		line.positionCount = shortenedList.Count;
		line.SetPositions(shortenedList.ToArray());

    }
}
