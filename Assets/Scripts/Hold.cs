using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold : MonoBehaviour
{

    SpriteRenderer sp;
    public Color holdColor;
    public Color flashColor;

    public float lifetime = .5f;
    public float delayBeforeReactivation = .25f;
    // public bool canHold;


    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    public void GetGrabbed()
    {
        sp.color = flashColor;
        StartCoroutine(ProcessHold());
    }

	public float elapsedPct;

    public IEnumerator ProcessHold()
    {
        float t1 = 0;
        float d1 = lifetime;

        while (t1 < d1)
        {
            elapsedPct = Mathf.Clamp01(t1 / d1);
			sp.color = Color.Lerp(sp.color, holdColor, elapsedPct);
            t1 += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        float t2 = 0;
        float d2 = delayBeforeReactivation;

        // canHold = false;

        while (t2 < d2)
        {
            float p = Mathf.Clamp01(t2 / d2);
            t2 += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        // canHold = true;
    }

    LineRenderer historyLine;

    public void AddLine(Hold target, bool setChild = false) {
        GameObject newLine = new GameObject();
        historyLine = newLine.AddComponent<LineRenderer>();
        

        Vector3[] linePositions = new Vector3[2];
        linePositions[0] = transform.position;
        linePositions[1] = target.transform.position;

        historyLine.positionCount = 2;
        historyLine.SetPositions(linePositions);
        historyLine.SetWidth(GlobalSettings.GameSettings.line_width, GlobalSettings.GameSettings.line_width);
        historyLine.numCapVertices = 90;

        if (setChild) {
            historyLine.transform.parent = target.transform;
        }
    }
}
