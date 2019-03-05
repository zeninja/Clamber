using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RingEffect : MonoBehaviour {

	LineRenderer line;
	int NUM_SEGMENTS = 1000;
	public Extensions.Property lineWidth;
	public Extensions.Property radius;

	float currentRadius;
	float currentLineWidth;
	public float duration;

	public Color defaultColor;
	bool ringTriggered;


	// Use this for initialization
	void Start () {
		line = GetComponent<LineRenderer>();
		line.useWorldSpace    = false;
		line.sortingLayerName = "UI";
	}
	
	// Update is called once per frame
	void Update () {
		HandleInput();
	}

	void HandleInput() {
		if(Input.GetKeyDown(KeyCode.C)) {
			TriggerRing(defaultColor);
		}

		if(ringTriggered) {
			UpdateLineWidthAndRadius(1);
			DrawRing();
		}
	}

	void HandleCatch() {
		TriggerRing(defaultColor);
	}

	public void TriggerRing(Color ballColor) {
		line.material.color = ballColor;
		line.enabled = true;
		StartCoroutine(AnimateRing(duration));
	}

	IEnumerator AnimateRing(float d) {
		float t = 0;

		while(t < d) {
			float p = t / d;

			UpdateLineWidthAndRadius(p);
			DrawRing();

			t += Time.fixedDeltaTime;
			yield return new WaitForFixedUpdate();
		}

		ringTriggered = true;
	}

	void UpdateLineWidthAndRadius(float percent) {
		currentRadius    = Extensions.GetSmoothStop5Range(radius,    percent);
		currentLineWidth = Extensions.GetSmoothStop5Range(lineWidth, percent);
	}

	void DrawRing()
    {
        float x;
        float y;
        float z = 10f;

        float angle = 20f;

        for (int i = 0; i < (NUM_SEGMENTS + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * currentRadius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * currentRadius;

			Vector3 linePos = new Vector3(x, y, z);

            line.positionCount = (NUM_SEGMENTS + 1);
            line.SetPosition(i, linePos);

            angle += (360f / NUM_SEGMENTS);
        }

        line.startWidth = currentLineWidth;
        line.endWidth   = currentLineWidth;
    }
}
