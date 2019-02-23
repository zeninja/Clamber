using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReHand : MonoBehaviour
{

    public float rotationSpeed = 5;

    Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Jump();
    }

    public float drag = -.15f;

    // void FixedUpdate() {
    // 	Vector2 force = drag * rb.velocity.normalized * rb.velocity.sqrMagnitude;
    // 	rb.AddForce(force);
    // }

    void Rotate()
    {
		#if UNITY_EDITOR
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
		#elif UNITY_IOS
		float inputHorizontal = Input.acceleration.z;
		#endif

        if (inputHorizontal != 0)
        {
            transform.Rotate(new Vector3(0, 0, -rotationSpeed * inputHorizontal));
        }
    }

    public float jumpForce;

    bool inputJumpStart;
    bool inputJumpEnd;

    void Jump()
    {
		#if UNITY_EDITOR
        inputJumpStart = Input.GetKeyDown(KeyCode.Space);
		#elif UNITY_IOS
		inputJumpStart = Input.touchCount == 1;
		#endif

        if (inputJumpStart)
        {
            Debug.Log("Jumping");
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            rb.gravityScale = 1;
        }

		#if UNITY_EDITOR
        inputJumpEnd = Input.GetKeyUp(KeyCode.Space);
		#elif UNITY_IOS
		inputJumpEnd = Input.touchCount == 0;
		#endif

        if (inputJumpEnd)
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
        }
    }
}
