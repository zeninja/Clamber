using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CircleEffect))]
public class CircleEffectEditor : Editor
{
    override public void OnInspectorGUI() {
        var circle = target as CircleEffect;

        circle.pointCount = EditorGUILayout.IntSlider("Point Count:", circle.pointCount , 1 , 1000);
        circle.lineWidth  = EditorGUILayout.Slider("Line width:", circle.lineWidth , 0 , 6);
        circle.radius     = EditorGUILayout.Slider("Radius:", circle.radius , 0 , 6);

        circle.dottedLine = GUILayout.Toggle(circle.dottedLine, "Dotted line:");

        if (circle.dottedLine) {
            circle.dottedLineSegments = EditorGUILayout.IntSlider("Segment Count:", circle.dottedLineSegments , 1 , 360);
            // circle.segmentPercentage  = EditorGUILayout.Slider("Segment %:", circle.segmentPercentage , 0 , 1);
        }
    }

}