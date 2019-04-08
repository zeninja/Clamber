using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroEnabler : MonoBehaviour
{
    public TutorialController tutorialController;

    bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!triggered)
            {
                triggered = true;
                Debug.Log("Enabling gyro");
                tutorialController.EnableGyro();
            }
        }
    }
}
