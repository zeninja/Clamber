using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{

    void Start()
    {
        lastJumpPos = transform.position;
    }

    public float rotationSpeed = 5f;

	enum HandState { onHold, inAir };
	HandState state = HandState.onHold;

    void Update()
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Jump());
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopCoroutine(Jump());
            EndJump();
        }

        if (currentHold != null)
        {
            transform.Translate(Vector2.down * currentHold.fallSpeed * Time.deltaTime, Space.World);
            lastJumpPos = transform.position;

            GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, Color.white, Time.time - lastJumpTime);
        }
        else
        {
            transform.position = lastJumpPos + jumpVector;
            lastJumpTime = Time.time;
        }

    }

	void FixedUpdate() {
		
	}

    float lastJumpTime;
    // public float jumpDistance = 3;
    Vector2 jumpVector;

    Vector2 lastJumpPos;

    public float jumpDuration = .2f;
    bool jumping;

    public IEnumerator Jump()
    {
        jumping = true;
        currentHold = null;

        float t = 0;
        float d = jumpDuration;

        while (t < d)
        {
            float p = Mathf.Clamp01(t / d);
            t += Time.fixedDeltaTime;
            jumpVector = transform.up * GameManager.distanceToNextHold * EZEasings.SmoothStop3(p);
            yield return new WaitForFixedUpdate();
        }
    }

    void EndJump()
    {
        jumpVector = Vector2.zero;
        lastJumpPos = transform.position;
        jumping = false;
		CheckForOverlap();
    }

    public float handRadius = .5f;

    void CheckForOverlap()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, handRadius, Vector2.zero);

        if (hit)
        {
            if (hit.collider.CompareTag("Hold"))
            {
                currentHold = hit.collider.gameObject.GetComponent<Hold>();
				currentHold.GetGrabbed();
            }
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    public Hold currentHold;
    // Hold nextHold;

    public void SetFirstHold(Hold h)
    {
        currentHold = h;
    }
}
