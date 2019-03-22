using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]
public class LineFrame : MonoBehaviour
{

    public float width = 100, height = 100;
    public float depth = 0;
    LineRenderer line;

    public int vertexCount = 100;
    public float lineWidth = .0125f;

    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    Vector3[] framePositions = new Vector3[4];

    // Update is called once per frame
    void Update()
    {
        SetFramePositions();
    }

    void SetFramePositions()
    {
        List<Vector3> corners = new List<Vector3>() {
                 new Vector3(-width / 2,  height / 2, depth),
                 new Vector3(-width / 2, -height / 2, depth),
                 new Vector3( width / 2, -height / 2, depth),
                 new Vector3( width / 2,  height / 2, depth)
        };

        // line.positionCount = vertexCount;
        GenerateLines(corners);
    }


    void GenerateLines(List<Vector3> colliderPts)
    {
        int ptsPerLine = vertexCount / colliderPts.Count;

        // line = gameObject.AddComponent<LineRenderer>();
        line.numCapVertices = 90;

        List<Vector3> completeList = new List<Vector3>();

        for (int i = 0; i < colliderPts.Count; i++)
        {
            for (int x = 0; x < ptsPerLine; x++)
            {
                int nextIndex = (i + 1) % colliderPts.Count;
                float pctAlongLine = (float)x / (float)ptsPerLine;
                completeList.Add(Vector3.Lerp(colliderPts[i], colliderPts[nextIndex], pctAlongLine));
            }
        }

        line.positionCount = ptsPerLine * colliderPts.Count;
        line.SetPositions(completeList.ToArray());

        line.startWidth = StyleManager.LINE_WIDTH;
        line.endWidth   = StyleManager.LINE_WIDTH;

        line.useWorldSpace = false;
    }
}