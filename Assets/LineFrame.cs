using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LineFrame : MonoBehaviour
{

    public float width = 100, height = 100;
	public float depth = 0;
    LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    Vector3[] framePositions = new Vector3[4];

    // Update is called once per frame
    void Update()
    {
        framePositions[0] = new Vector3(-width / 2,  height / 2, depth);
        framePositions[1] = new Vector3(-width / 2, -height / 2, depth);
        framePositions[2] = new Vector3( width / 2, -height / 2, depth);
        framePositions[3] = new Vector3( width / 2,  height / 2, depth);

        line.SetPositions(framePositions);
    }
}
