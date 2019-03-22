using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{

    GrabDisplay grabDisplay;
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

    public enum HandState { OnHold, Jumping, GrabSuccess, GrabFailed, Falling };
    public static HandState state = HandState.OnHold;
    bool hasStarted = false;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        grabDisplay = GetComponent<GrabDisplay>();

        SetColor();

        AddGrabPositionToDisplay();
    }

    SpriteRenderer sp;

    void SetColor()
    {
        sp.color = handColor;
    }


    // Update is called once per frame
    void Update()
    {
        Rotate();

        if (SettingsDisplay.active) { return; }

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
                grabDisplay.HandleJump();
                break;
            case HandState.OnHold:
                canJump = true;
                break;
            case HandState.GrabSuccess:
                SetState(HandState.OnHold);
                break;
            case HandState.GrabFailed:
                SetState(HandState.Falling);
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

    public float MAX_JUMP_FORCE = 250;
    public float MIN_JUMP_FORCE = 200;

    public void AdjustJumpForce() {
        jumpForce++;
        if (jumpForce > MAX_JUMP_FORCE) {
            jumpForce = MIN_JUMP_FORCE;
        }
        
    }

    Hold currentHold;
    bool canJump = true;

    public bool useBetterJump = false;

    void ProcessJumpInput()
    {
        jumpForce = GlobalSettings.GameSettings.jump_force;

        switch (GlobalSettings.GameSettings.use_alt_ctrl_scheme)
        {
            case false:
                // default controls
                if (InputManager.inputJumpStart && canJump)
                {
                    Jump();
                }

                if (InputManager.inputJumpHeld)
                {
                    grabDisplay.HandleJump();
                }

                if (InputManager.inputJumpEnd)
                {
                    TryToGrab();
                }
                break;

            case true:
                if (InputManager.inputJumpStart)
                {
                    TryToGrab();
                }

                if (InputManager.inputJumpEnd && canJump)
                {
                    Jump();
                }
                break;
        }
    }

    public float fallMultiplier = 2;
    public float lowJumpMultiplier = 3;

    void Jump()
    {
        // Debug.Log("Jumping");
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        rb.gravityScale = 1;

        sp.enabled = false;

        SetState(HandState.Jumping);
    }

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

        sp.enabled = true;

        grabDisplay.HandleGrab(true);

        AddGrabPositionToDisplay();

        SetState(HandState.GrabSuccess);
    }

    void HandleGrabFailed()
    {
        // should probably.. add some mechanics to this...
        // grab "cooldown"?
        // gets worse the more grab attempts you miss?
        // grabDisplay.HandleGrabFailed();

        sp.enabled = false;
        grabDisplay.HandleGrab(false);

        SetState(HandState.GrabFailed);
    }

    public void HandleLevelEnd()
    {
        rb.gravityScale = 0;
    }


    void AddGrabPositionToDisplay()
    {
        grabPositionHistory.Add(transform.position);
        DrawGrabHistory();
    }

    List<Vector3> grabPositionHistory = new List<Vector3>();

    float pathWidth = .0125f;

    void DrawGrabHistory()
    {
        GameObject history = new GameObject();
        LineRenderer l = history.AddComponent<LineRenderer>();
        l.positionCount = grabPositionHistory.Count;
        l.SetPositions(grabPositionHistory.ToArray());
        l.startWidth = pathWidth;
        l.endWidth   = pathWidth;
    }

}
