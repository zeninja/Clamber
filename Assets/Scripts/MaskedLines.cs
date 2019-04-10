using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskedLines : MonoBehaviour
{
    public LineRenderer linePrefab;
    public int lineCount = 10;
    
    public float lineSpread;
    public float lineWidth;
    public float lineLength = 2;

    List<LineRenderer> lines = new List<LineRenderer>();

    void Start() {

        for(int i = 0; i < lineCount; i++) {
            LineRenderer newLine = Instantiate(linePrefab);

            newLine.SetWidth(lineWidth, lineWidth);
            
            newLine.SetPosition(0, new Vector3(0, lineLength/2, 0));
            newLine.SetPosition(1, new Vector3(0, -lineLength/2, 0));

            float x = (float)i / (float)(lineCount-1) * lineSpread - lineSpread / 2;
            newLine.transform.position = new Vector3(x, 0, 0);

            newLine.transform.parent = transform;

            lines.Add(newLine);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        UpdateLines();
    }

    public float lineRotation = 0;
    public float rotationSpeed = 10;

    void UpdateLines() {
        lineRotation += rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, lineRotation));
    }
}
