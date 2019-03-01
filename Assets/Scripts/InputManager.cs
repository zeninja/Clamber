using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	public static bool inputJumpStart;
	public static bool inputJumpHeld;
	public static bool inputJumpEnd;
	public static float inputHorizontal;
	public static float MAX_DEVICE_TILT_ANGLE = 20;

	void Update() {
		HandleInput();
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
        inputJumpHeld   = Input.GetKey    (KeyCode.Space);
        inputJumpEnd    = Input.GetKeyUp  (KeyCode.Space);
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

        if (Input.touchCount == 0) {
            inputJumpEnd = false;
        }
        
        float angle = GyroToUnity(Input.gyro.attitude).eulerAngles.z;
        float mappedAngle = Extensions.mapRangeMinMax(90 - MAX_DEVICE_TILT_ANGLE, 90 + MAX_DEVICE_TILT_ANGLE, -1, 1, angle);

        inputHorizontal = mappedAngle;
    }

    Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }


}
