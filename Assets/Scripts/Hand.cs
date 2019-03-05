using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{

    public Color handColor;

    private static Hand instance;
    public static Hand GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public float rotationSpeed = 5;

    Rigidbody2D rb;

    public enum HandState { OnHold, Jumping };
    public static HandState state = HandState.OnHold;
    bool hasStarted = false;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetColor();
    }

    void SetColor() {
        GetComponent<SpriteRenderer>().color = handColor;
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
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("KillTrigger"))
        {
            GameManager.GetInstance().Reset();
        }

        if (other.CompareTag("LevelEnd"))
        {
            // GameManager.GetInstance().Reset();
            GameManager.GetInstance().HandleLevelEnd();
        }
    }

    void SetState(HandState newState)
    {
        state = newState;
        switch (state)
        {
            case HandState.Jumping:
                canJump = false;
                CheckFirstInput();
                break;
            case HandState.OnHold:
                canJump = true;
                break;
        }
    }

    public static bool OnHold()
    {
        return state == HandState.OnHold;
    }

    public static bool Jumping()
    {
        return state == HandState.Jumping;
    }

    void Rotate()
    {
        float angle = InputManager.inputHorizontal * LevelManager.HOLD_SPREAD;

        Vector3 targetRotation = new Vector3(0, 0, angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRotation), rotationSpeed * Time.deltaTime);
        currentRotation = transform.rotation.eulerAngles;
    }

    void CheckFirstInput()
    {
        // TEMP
        // Replace this with better code in the game manager or something
        if (!hasStarted)
        {
            hasStarted = true;
            GameManager.GetInstance().StartBurn();
        }
    }


    public static Vector3 currentRotation;

    public float jumpForce;

    Hold currentHold;
    bool canJump = true;

    public bool useBetterJump = false;

    void ProcessJumpInput()
    {
        if (InputManager.inputJumpStart && canJump)
        {
            // Debug.Log("Jumping");
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
            // Debug.Log("Jumping");
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

        if (hit)
        {
            // Grab succeeded
            HandleGrabSuccess(hit);
        }
        else
        {
            // Grab failed
            HandleGrabFailed();
        }
    }

    void HandleGrabSuccess(RaycastHit2D hit)
    {
        currentHold = hit.collider.gameObject.GetComponent<Hold>();
        currentHold.GetGrabbed();

        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;

        SetState(HandState.OnHold);
    }

    void HandleGrabFailed()
    {

    }

    public void HandleLevelEnd()
    {
        rb.gravityScale = 0;
    }

    // public float handRadius;

    // void OnGUI()
    // {

    //     // GUI.Label(new Rect(0, 0, ScreenInfo.x, ScreenInfo.y), "gyro attitude:   " + Input.gyro.attitude + "\n" +
    //     //                                                       "euler angles:    " + Input.gyro.attitude.eulerAngles + "\n" +
    //     //                                                       "inputHorizontal: " + InputManager.inputHo rizontal.ToString());


    // }

    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.white;
    //     Gizmos.DrawWireSphere(transform.position, GetComponent<CircleCollider2D>().radius / 2);

    //     Gizmos.color = Color.green;
    //     Gizmos.DrawLine(transform.position, transform.position + transform.up);
    // }

}
