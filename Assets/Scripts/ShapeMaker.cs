using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]
public class ShapeMaker : MonoBehaviour
{
    [Range(3, 100)]
    public int shapePointCount = 100;
    [Range(3, 100)]
    public int numVertices = 100;
    // [Range(.125f, 6f)]
    public float radius;
    // [Range(.125f, 6f)]

    public float lineWidth = .1f;
    List<LineRenderer> lines;
    LineRenderer line;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = (shapePointCount);
        line.useWorldSpace = false;
        line.enabled = false;
        // Debug.Log("aWOKE");
        
    }

    void Start()
    {
        lines = new List<LineRenderer>();
        CalculateRadius();
        MakePoints();
    }

    public void SetColor(Color newColor) {
        GetComponent<LineRenderer>().material.color = newColor;

        if(transform.childCount > 0) {
            LineRenderer[] childLines = GetComponentsInChildren<LineRenderer>();

            foreach(LineRenderer l in childLines) {
                l.material.color = newColor;
            }
        }
    }

    void Update()
    {
        // probably don't need this in update...?
        CalculateRadius();
    }

    float adjustedRadius;

    void CalculateRadius()
    {
        adjustedRadius = Mathf.Clamp(radius - (lineWidth / 2), 0, radius / 2);
    }

    void MakePoints()
    {
        Vector3[] positions = new Vector3[shapePointCount + 1];

        float x, y, z = -1f;
        float angle = 0;

        for (int i = 0; i < (shapePointCount + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * adjustedRadius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * adjustedRadius;

            positions[i] = new Vector3(x, y, z);
            angle += 360f / shapePointCount;
        }

        line.SetWidth(lineWidth, lineWidth);
        line.positionCount = shapePointCount + 1;
        line.SetPositions(positions);
        line.enabled = true;

        // Debug.Log("Made points");
    }

    public bool dottedLine = false;
    public int dottedLineSegments = 1;
    public float segmentPercentage = 1;

}



