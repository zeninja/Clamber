using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBlock : MonoBehaviour
{
    Collider2D c2D;

    // Use this for initialization
    void Start()
    {
        c2D = GetComponent<Collider2D>();
        settings = GetComponentInParent<SettingsDisplay>();
    }

    Vector2 startPoint;
    Vector2 diff;
    int inputVertical;
    int inputHorizontal;
    SettingsDisplay settings;

    // Update is called once per frame
    void Update()
    {
        HandleTouchInput();
        // HandleMouseInput();
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {

                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {

                    // Check where the finger is
                    Vector2 touchPos = Extensions.ScreenToWorld(Input.touches[i].position);

                    if (Physics2D.OverlapPoint(touchPos) == c2D)
                    {
                        // Hit the collider
                        startPoint = touchPos;
                    }

                }

                if (Input.GetTouch(i).phase == TouchPhase.Moved || Input.GetTouch(i).phase == TouchPhase.Stationary)
                {

                    // Check where the finger is
                    diff = Extensions.ScreenToWorld(Input.touches[i].position);
                    // ProcessDrag(diff);
                    // settings.HandleInput(diff.x);
                    // settings.HandleInput();
                }

            }
        }
    }

    bool validStart;

    void HandleMouseInput()
    {
        diff = Vector2.zero;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 touchPos = Extensions.ScreenToWorld(Input.mousePosition);
            if(Physics2D.OverlapPoint(touchPos) == c2D) {
                validStart = true;
                startPoint = Input.mousePosition;
            } else {
                validStart = false;
            }
        }

        if (Input.GetMouseButton(0) && validStart)
        {
            diff = (Vector2)Input.mousePosition - startPoint;
        }

        processedInput = ProcessDrag(diff);
    }

    public float processedInput;

    float ProcessDrag(Vector2 diff)
    {
        if (diff.x < -30)
        {
            return -1;
        }
        else
        if (diff.x > 30)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
