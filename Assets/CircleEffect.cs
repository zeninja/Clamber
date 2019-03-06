using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]
public class CircleEffect : MonoBehaviour
{
    int NUM_SEGMENTS = 100;

    public float radius;
    public float lineWidth = .1f;
    LineRenderer line;
    float startTime;

    void Awake()
    {
        startTime = Time.time;
        line               = gameObject.GetComponent<LineRenderer>();
        line.positionCount = (NUM_SEGMENTS + 1);
        line.useWorldSpace = false;
        line.enabled       = false;
    }

    void Start()
    {
        CreatePoints();
    }

    void Update()
    {
        CalculateRadius();
        CreatePoints();
    }

    float adjustedRadius;

    void CalculateRadius() {
        adjustedRadius = Mathf.Clamp(radius - (lineWidth / 2), 0, radius / 2);
    }

    void CreatePoints()
    {
        float x;
        float y;
        float z = -1f;

        float angle = 20f;

        for (int i = 0; i < (NUM_SEGMENTS + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * adjustedRadius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * adjustedRadius;

            line.positionCount = (NUM_SEGMENTS + 1);
            line.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / NUM_SEGMENTS);
        }

        line.startWidth = lineWidth;
        line.endWidth   = lineWidth;
        line.enabled = true;
    }
}