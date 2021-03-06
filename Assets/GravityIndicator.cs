﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityIndicator : MonoBehaviour
{


    public List<LineRenderer> lines;

    public int x = 5, y = 45;

    public float grid_w, grid_h;

    public float lineWidth = .0125f;

    void Start()
    {
        SpawnLines();
    }

    void Update()
    {
		UpdateLines();
    }

    void SpawnLines()
    {
        lines = new List<LineRenderer>();

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                LineRenderer l = new GameObject().AddComponent<LineRenderer>().GetComponent<LineRenderer>();
                lines.Add(l);
                l.transform.parent = transform;
                l.transform.position = new Vector3((grid_w / 2) * ((float)i / (float)x), (grid_h / 2) * ((float)j / (float)y), 0);
                l.SetWidth(lineWidth, lineWidth);
                l.numCapVertices = 90;
            }
        }
    }

    void UpdateLines()
    {
        Vector3[] positions = new Vector3[2];

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
				// Debug.Log("index: " + (j * x + i));

				LineRenderer l = lines[j * x + i];
                l.transform.position = new Vector3((grid_w / 2) * ((float)i / (float)x), (grid_h / 2) * ((float)j / (float)y), 0);
				l.transform.position += new Vector3(-x / 2, 0, 0);

                positions[0] = l.transform.position;
                positions[1] = l.transform.position;

                l.positionCount = 2;
                l.SetPositions(positions);

            }
        }
    }

}
