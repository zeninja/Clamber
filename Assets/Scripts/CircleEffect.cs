using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]
public class CircleEffect : MonoBehaviour
{
    public int pointCount = 100;
    public float radius;
    public float lineWidth = .1f;
    List<LineRenderer> lines;
    LineRenderer line;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = (pointCount);
        line.useWorldSpace = false;
        line.enabled = false;
    }

    void Start()
    {
        lines = new List<LineRenderer>();
    }

    void Update()
    {
        CalculateRadius();

        MakeCircle();
        // CreatePoints();
    }

    float adjustedRadius;

    void CalculateRadius()
    {
        adjustedRadius = Mathf.Clamp(radius - (lineWidth / 2), 0, radius / 2);
    }

    void MakeCircle()
    {
        Vector3[] positions = new Vector3[pointCount + 1];

        float x, y, z = -1f;
        float angle = 0;

        for (int i = 0; i < (pointCount + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * adjustedRadius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * adjustedRadius;

            positions[i] = new Vector3(x, y, z);
            angle += 360f / pointCount;
        }

        line.SetWidth(lineWidth, lineWidth);
        line.positionCount = pointCount + 1;
        line.SetPositions(positions);
        line.enabled = true;
    }




    // void CreatePoints()
    // {
    //     float x;
    //     float y;
    //     float z = -1f;

    //     float angle = 0f;

    //     for (int i = 0; i < pointCount; i++)
    //     {
    //         x = Mathf.Sin(Mathf.Deg2Rad * angle) * adjustedRadius;
    //         y = Mathf.Cos(Mathf.Deg2Rad * angle) * adjustedRadius;

    //         positions[i] = new Vector3(x, y, z);
    //         angle += (360f / lines.Count);
    //     }
    //     line.SetWidth(lineWidth, lineWidth);
    //     line.positionCount = pointCount + 1;
    //     line.SetPositions(positions);
    //     line.enabled = true;
    // }

    public bool dottedLine = false;
    public int dottedLineSegments = 1;
    public float segmentPercentage = 1;

}



