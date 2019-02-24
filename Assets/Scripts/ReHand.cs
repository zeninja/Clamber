﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReHand : MonoBehaviour
{

    public float rotationSpeed = 5;

    Rigidbody2D rb;

    public enum HandState { OnHold, Jumping };
    HandState state;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        Rotate();
        ProcessJumpInput();
    }

    public bool useKeyboard;

    void HandleInput()
    {
        HandleMobileInput();
        #if UNITY_EDITOR
        if (useKeyboard)
        {
            HandleKeyboardInput();
        }
        #endif
    }

    void HandleKeyboardInput()
    {
        inputJumpStart  = Input.GetKeyDown(KeyCode.Space);
        inputJumpEnd    = Input.GetKeyUp(KeyCode.Space);
        inputHorizontal = Input.GetAxisRaw("Horizontal");
    }

    void HandleMobileInput()
    {

        if (Input.touchCount > 0)
        {
            inputJumpStart = Input.touches[0].phase == TouchPhase.Began;
            inputJumpHeld  = Input.touches[0].phase == TouchPhase.Moved || Input.touches[0].phase == TouchPhase.Stationary;
            inputJumpEnd   = Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled;
        }
        inputHorizontal = Input.acceleration.x;
    }

    void SetState(HandState newState)
    {
        state = newState;
        switch (state)
        {
            case HandState.Jumping:
                canJump = false;
                break;
            case HandState.OnHold:
                canJump = true;
                break;
        }
    }

    float inputHorizontal;
    void Rotate()
    {
        Vector3 targetRotation = new Vector3(0, 0, inputHorizontal * -HoldSpawner.spreadAngle);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRotation), rotationSpeed * Time.deltaTime);
    }

    public float jumpForce;

    bool inputJumpStart;
    bool inputJumpHeld;
    bool inputJumpEnd;
    Hold currentHold;
    bool canJump;

    void ProcessJumpInput()
    {
        if (inputJumpStart && canJump)
        {
            // Debug.Log("Jumping");
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            rb.gravityScale = 1;
            SetState(HandState.Jumping);
        }

        if (inputJumpEnd)
        {
            TryToGrab();
        }
    }


    void TryToGrab()
    {
        // Debug.Log("Grabbing");
        // Vector2 handSize = GetComponent<BoxCollider2D>().size;
        // RaycastHit2D hit = Physics2D.BoxCast(transform.position, handSize, transform.eulerAngles.z, Vector2.zero);
        // GetComponent<SpriteRenderer>().color = Color.white;
        // if (hit)
        // {
        //     if (hit.collider.CompareTag("Hold"))
        //     {
        //         Debug.Log("HIT SOMETHING");
        //         currentHold = hit.collider.gameObject.GetComponent<Hold>();
        //         currentHold.GetGrabbed();
        //         rb.gravityScale = 0;
        //         rb.velocity = Vector2.zero;
        //         GetComponent<SpriteRenderer>().color = Color.green;
        //         SetState(HandState.OnHold);
        //     }
        // }
        // else
        // {
        //     GetComponent<SpriteRenderer>().color = Color.red;
        // }

        float handRadius = GetComponent<CircleCollider2D>().radius / 2;
        
        int layerMask = 1 << LayerMask.NameToLayer("Hold");
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, handRadius, Vector2.zero, 0, layerMask);

        Debug.Log("Grabbing");

        if (hit)
        {
            currentHold = hit.collider.gameObject.GetComponent<Hold>();
            currentHold.GetGrabbed();

            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;

            GetComponent<SpriteRenderer>().color = Color.green;
            SetState(HandState.OnHold);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    // public float handRadius;

    void OnGUI()
    {

        GUI.Label(new Rect(Screen.width / 2, 0, 100, 100), Input.acceleration.ToString());
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, GetComponent<CircleCollider2D>().radius / 2);
    }

}
