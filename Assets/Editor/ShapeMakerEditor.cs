using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
[CustomEditor(typeof(ShapeMaker))]
public class ShapeMakerEditor : Editor
{
     public void OnInspectorGUI() {
        var circle = target as ShapeMaker;

        circle.shapePointCount = EditorGUILayout.IntSlider("Point Count:", circle.shapePointCount , 3 , 100);
        circle.numVertices     = EditorGUILayout.IntSlider("Vertex Count:", circle.numVertices, 3, 1000);
        circle.lineWidth       = EditorGUILayout.Slider("Line width:", circle.lineWidth , 0 , 6);
        circle.radius          = EditorGUILayout.Slider("Radius:", circle.radius , 0 , 6);
        circle.dottedLine      = GUILayout.Toggle(circle.dottedLine, "Dotted line:");

        if (circle.dottedLine) {
            circle.dottedLineSegments = EditorGUILayout.IntSlider("Segment Count:", circle.dottedLineSegments , 1 , 360);
            // circle.segmentPercentage  = EditorGUILayout.Slider("Segment %:", circle.segmentPercentage , 0 , 1);
        }
    }

}