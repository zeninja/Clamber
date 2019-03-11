using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public static bool inputJumpStart;
    public static bool inputJumpHeld;
    public static bool inputJumpEnd;
    public static float inputHorizontal;

    public static float MIN_TILT_ANGLE = 2.5f;
    public static float DEVICE_TILT_ANGLE = 20;
    public static float MAX_TILT_ANGLE = 45;

    Vector3 gyro;

    void Awake()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
        else
        {
            //
            Debug.Log("- - - NO GYRO - - -");
            useKeyboard = true;
        }
    }

    void Update()
    {
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
        inputJumpStart = Input.GetKeyDown(KeyCode.Space);
        inputJumpHeld = Input.GetKey(KeyCode.Space);
        inputJumpEnd = Input.GetKeyUp(KeyCode.Space);
        inputHorizontal = Input.GetAxisRaw("Horizontal");
    }

    void HandleMobileInput()
    {
        if (Input.touchCount > 0)
        {
            inputJumpStart = Input.touches[0].phase == TouchPhase.Began;
            inputJumpHeld = Input.touches[0].phase == TouchPhase.Moved || Input.touches[0].phase == TouchPhase.Stationary;
            inputJumpEnd = Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled;
        }

        if (Input.touchCount == 0)
        {
            inputJumpEnd = false;
        }

        CalculateGyroInput();
    }

    void CalculateGyroInput()
    {
        gyro = Input.gyro.attitude.eulerAngles;

        Quaternion referenceRotation = Quaternion.identity;
        Quaternion deviceRotation = GyroToUnity(Input.gyro.attitude);
        Quaternion eliminationOfXY = Quaternion.Inverse(
            Quaternion.FromToRotation(referenceRotation * Vector3.forward, deviceRotation * Vector3.forward)
        );

        Quaternion rotationZ = eliminationOfXY * deviceRotation;
        roll = rotationZ.eulerAngles.z;

        if (roll < 180)
        {
            inputHorizontal = Extensions.mapRange(0, DEVICE_TILT_ANGLE, 0, 1, roll);
        }
        else
        {
            inputHorizontal = Extensions.mapRange(360, 360 - DEVICE_TILT_ANGLE, 0, -1, roll);
        }
    }


    public static float roll;

    Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

    public static void AdjustDeviceAngle()
    {
        DEVICE_TILT_ANGLE += 2.5f;
        if (DEVICE_TILT_ANGLE > MAX_TILT_ANGLE)
        {
            DEVICE_TILT_ANGLE = MIN_TILT_ANGLE;
        }
    }

    public static void ResetGyro()
    {
        Input.gyro.enabled = false;
        Input.gyro.enabled = true;
    }
}
