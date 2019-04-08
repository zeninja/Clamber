using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskedDots : MonoBehaviour
{
    public LineRenderer linePrefab;
    public int xDots, yDots = 10;
    
    float xSpread, ySpread;
    public float dotWidth;
    public int yDotOffset = 3;


    public float spreadPct = 1;

    List<LineRenderer> dots = new List<LineRenderer>();

    void Start() {

        xSpread = xDots /10f;
        ySpread = yDots /10f;

        StartCoroutine(SpawnDots());
    }

    IEnumerator SpawnDots()
    {
        for (int j = 0; j < yDots; j++)
        {
            for (int i = 0; i < xDots; i++)
            {
                float x = (float)i / (float)(xDots - 1) * xSpread - xSpread / 2;
                float y = (float)j / (float)(yDots - 1) * ySpread - (yDotOffset * ySpread / (float)(yDots - 1)); 
                // y -= 

                // float x = i / (xDots - 1);

                LineRenderer l = Instantiate(linePrefab);
                l.transform.position = new Vector3(x, y, 0);

                // l.transform.parent    = transform;
                // l.transform.position  = new Vector3((x / 2) * ((float)i / (float)xDots), (y / 2) * ((float)j / (float)yDots), 0);
				// l.transform.position += new Vector3(-xDots / 2, 0, 0);

                l.SetWidth(dotWidth, dotWidth);
                l.numCapVertices = 90;

				Vector3[] positions = new Vector3[2];
                positions[0] = l.transform.position;
				positions[1] = l.transform.position;
                l.SetPositions(positions);

                dots.Add(l);

                l.transform.parent = transform;

            }
            yield return new WaitForEndOfFrame();
        }
    }

    
    
    // Update is called once per frame
    void Update()
    {
        // UpdateLines();
    }

    public float lineRotation = 0;
    public float rotationSpeed = 10;

    // void UpdateLines() {
    //     lineRotation += rotationSpeed * Time.deltaTime;

    //     transform.rotation = Quaternion.Euler(new Vector3(0, 0, lineRotation));
    // }

    public void EnableAllHolds() {
        for(int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
