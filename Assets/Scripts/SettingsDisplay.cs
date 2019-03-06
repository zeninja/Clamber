using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingsDisplay : MonoBehaviour
{

    public string[] labels;
    public string[] values;

    TextMeshProUGUI title;
    TextMeshProUGUI[] tmp_labels;
    TextMeshProUGUI[] tmp_values;
    public TextMeshProUGUI values_TEMP; // temp, should switch to line by line TMP
    public TextMeshProUGUI input;


    int selectionIndex;

    void Start()
    {

        tmp_labels = new TextMeshProUGUI[labels.Length];
        tmp_values = new TextMeshProUGUI[values.Length];
        block = GetComponentInChildren<UIBlock>();
        // SpawnText();
    }

    // void SpawnText() {
    // 	for (int i = 0; i < labels.Length; i++) {
    // 		tmp_labels[i] = new GameObject().AddComponent<TextMeshProUGUI>();
    // 		tmp_values[i] = new GameObject().AddComponent<TextMeshProUGUI>();
    // 	}
    // }

    // void SelectText(int index) {
    // 	for (int i = 0; i < labels.Length; i++) {
    // 		tmp_labels[i].fontStyle = index == i ? FontStyles.Underline : FontStyles.Normal;
    // 		tmp_values[i].fontStyle = index == i ? FontStyles.Underline : FontStyles.Normal;
    // 	}
    // }

    UIBlock block;

    void Update()
    {
        // HandleInput(block.processedInput);
        HandleInput();
    }

    public void HandleInput()
    {
        // show input in the bottom right of box
        // input.text = "\n\n\n\n" + currentInput;


    }

    float nextAngTime;
	float nextAngDelay = .125f;

    public void IncrementDeviceAngle()
    {
        if (Time.time > nextAngTime)
        {
            InputManager.AdjustDeviceAngle();
            // InputManager.DEVICE_TILT_ANGLE = Mathf.Clamp(InputManager.DEVICE_TILT_ANGLE += currentInput, InputManager.MIN_TILT_ANGLE, InputManager.MAX_TILT_ANGLE);
            values_TEMP.text = "\n" + InputManager.DEVICE_TILT_ANGLE.ToString();
			nextAngTime = Time.time + nextAngDelay;
        }

    }
}
