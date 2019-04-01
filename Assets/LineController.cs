using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LineController : MonoBehaviour
{
    [Range(0, 1)]
    public float completion = 0;

    List<Vector3> completePositions = new List<Vector3>();
    List<Vector3> truncatedPositions = new List<Vector3>();


    LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        Invoke("GetLinePts", Time.deltaTime);
    }

    void GetLinePts()
    {
        Vector3[] oldPositions = new Vector3[line.positionCount];
        line.GetPositions(oldPositions);
        completePositions = oldPositions.ToList();
    }

    void LateUpdate()
    {
        DrawShortenedLine(completion);
    }

    public enum LineType { Progressive, Centered };
    public LineType lineType = LineType.Progressive;

    void DrawShortenedLine(float amt)
    {
        if (completePositions.Count == 0) { return; }

        int shortenedCount = Mathf.RoundToInt(amt * completePositions.Count);


        switch (lineType)
        {
            case LineType.Progressive:
                truncatedPositions = completePositions.GetRange(0, shortenedCount);
                break;
            case LineType.Centered:
                truncatedPositions = completePositions.GetRange(truncatedPositions.Count / 2 - shortenedCount / 2, shortenedCount);
                break;
        }

        line.positionCount = shortenedCount;
        line.SetPositions(truncatedPositions.ToArray());
    }

}
