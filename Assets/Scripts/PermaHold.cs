using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PermaHold : MonoBehaviour
{
    public float width, height;
    public GameObject quad;

    public float dotScalar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(width, height, 0);
        quad.transform.localScale = new Vector3(width, height, 0) * dotScalar;
        // Debug.Log(quad.transform.localScale);

        
    }
}
