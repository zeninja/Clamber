using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReHand : MonoBehaviour
{

    public float rotationSpeed = 5;

    Rigidbody2D rb;

    public enum HandState { OnHold, Jumping };
    HandState state = HandState.OnHold;
    // bool hasStarted = false;

    public static float SPREAD_ANGLE;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        Rotate();

        if (useBetterJump)
        {
            BetterJump();
        }
        else
        {
            ProcessJumpInput();
        }

        SPREAD_ANGLE = HoldSpawner.HOLD_SPREAD;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("KillTrigger"))
        {
            ReGameManager.GetInstance().Reset();
        }

        if (other.CompareTag("LevelEnd"))
        {
            ReGameManager.GetInstance().Reset();
            // ReGameManager.GetInstance().HandleLevelEnd();
        }
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

    void Rotate()
    {
        float rotationAmt = InputManager.inputHorizontal;
        rotationAmt = Extensions.mapRangeMinMax(-1, 1, -SPREAD_ANGLE, SPREAD_ANGLE, rotationAmt);

        Vector3 targetRotation = new Vector3(0, 0, rotationAmt);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRotation), rotationSpeed * Time.deltaTime);
        currentRotation = transform.rotation.eulerAngles;
    }


    public static Vector3 currentRotation;

    public float jumpForce;

    // bool inputJumpStart;
    // bool inputJumpHeld;
    // bool inputJumpEnd;
    Hold currentHold;
    bool canJump = true;

    public bool useBetterJump = false;

    void ProcessJumpInput()
    {
        if (InputManager.inputJumpStart && canJump)
        {
            Debug.Log("Jumping");
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            rb.gravityScale = 1;
            SetState(HandState.Jumping);
        }

        if (InputManager.inputJumpEnd)
        {
            TryToGrab();
        }
    }

    public float fallMultiplier = 2;
    public float lowJumpMultiplier = 3;

    void BetterJump()
    {

        if (InputManager.inputJumpStart && canJump)
        {
            Debug.Log("Jumping");
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            SetState(HandState.Jumping);
        }


        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;

        }
        else if (rb.velocity.y > 0 && !InputManager.inputJumpHeld)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }


    void TryToGrab()
    {
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

        GUI.Label(new Rect(0, 0, ScreenInfo.x, ScreenInfo.y), "gyro attitude:   " + Input.gyro.attitude + "\n" +
                                                                "euler angles:    " + Input.gyro.attitude.eulerAngles + "\n" +
                                                                "inputHorizontal: " + InputManager.inputHorizontal.ToString());


    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, GetComponent<CircleCollider2D>().radius / 2);

        // Vector3 toVec = 
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.AngleAxis(-SPREAD_ANGLE, Vector3.back) * Vector2.up);
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.AngleAxis(SPREAD_ANGLE, Vector3.back) * Vector2.up);


        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.up);
    }

}
