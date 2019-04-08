using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LineController : MonoBehaviour
{

    public bool animateOnEnable;
    [Range(0, 1)]
    public float completion = 0;

    List<Vector3> completePositions = new List<Vector3>();
    List<Vector3> truncatedPositions = new List<Vector3>();


    LineRenderer line;

    void OnEnable()
    {
        if (animateOnEnable && !animating)
        {
            animating = true;
            StartCoroutine(DrawIn());
        }
    }

    public float drawInDuration;
    bool animating;

    IEnumerator DrawIn()
    {
        float t = 0;
        // Debug.Break();
        while (t < drawInDuration)
        {
            t += Time.fixedDeltaTime;
            float p = Mathf.Clamp01(t / drawInDuration);
            completion = EZEasings.Linear(p);
            yield return new WaitForFixedUpdate();
        }

        animating = false;
    }

    public void Undraw() {
        StartCoroutine(DrawOut());
    }

    IEnumerator DrawOut()
    {
        float t = 0;
        while (t < drawInDuration)
        {
            t += Time.fixedDeltaTime;
            float p = Mathf.Clamp01(1 - t / drawInDuration);
            completion = EZEasings.Linear(p);
            yield return new WaitForFixedUpdate();
        }

        animating = false;
    }

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

    void Update()
    {
        DrawShortenedLine(completion);
    }

    public enum LineType { Progressive, Centered };
    public LineType lineType = LineType.Progressive;

    void DrawShortenedLine(float amt)
    {
        if (completePositions.Count == 0) { return; }
        int shortenedCount = Mathf.RoundToInt(amt * completePositions.Count);
        // Debug.Log("complete: " + completePositions.Count + "\n" + "shortened: " + shortenedCount + "\n" + "truncated: " + truncatedPositions.Count);

        switch (lineType)
        {
            case LineType.Progressive:
                // if(completePositions.Count > shortenedCount) {
                truncatedPositions = completePositions.GetRange(0, shortenedCount);
                // }
                break;
            case LineType.Centered:
                truncatedPositions = completePositions.GetRange(truncatedPositions.Count / 2 - shortenedCount / 2, shortenedCount);
                break;
        }

        line.positionCount = truncatedPositions.Count;
        line.SetPositions(truncatedPositions.ToArray());
    }

}
