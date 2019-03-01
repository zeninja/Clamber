using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReHand : MonoBehaviour
{

    public float rotationSpeed = 5;

    Rigidbody2D rb;

    public enum HandState { OnHold, Jumping };
    HandState state = HandState.OnHold;
    // float spreadAngle;
    bool hasStarted = false;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        Rotate();
        ProcessJumpInput();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("KillTrigger")) {
            ReGameManager.GetInstance().Reset();
        }

        if (other.CompareTag("LevelEnd")) {
            ReGameManager.GetInstance().HandleLevelEnd();
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

    void Rotate()
    {
        float rotationAmt = InputManager.inputHorizontal;
        rotationAmt = Extensions.mapRangeMinMax(-1, 1, -HoldSpawner.spreadAngle, HoldSpawner.spreadAngle, rotationAmt);

        Vector3 targetRotation = new Vector3(0, 0, rotationAmt);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRotation), rotationSpeed * Time.deltaTime);
        currentRotation = transform.rotation.eulerAngles;

        Debug.Log(currentRotation);
    }


    public static Vector3 currentRotation;

    public float jumpForce;

    // bool inputJumpStart;
    // bool inputJumpHeld;
    // bool inputJumpEnd;
    Hold currentHold;
    bool canJump = true;

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

        GUI.Label(new Rect(0, 0, ScreenInfo.x, ScreenInfo.y),   "gyro attitude:   " + Input.gyro.attitude + "\n" +
                                                                "euler angles:    " + Input.gyro.attitude.eulerAngles + "\n" +
                                                                "inputHorizontal: " + InputManager.inputHorizontal.ToString());

        
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, GetComponent<CircleCollider2D>().radius / 2);

        // Vector3 toVec = 
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.AngleAxis(-HoldSpawner.spreadAngle, Vector3.back) * Vector2.up );
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.AngleAxis( HoldSpawner.spreadAngle, Vector3.back) * Vector2.up );


        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.up);
    }

}
