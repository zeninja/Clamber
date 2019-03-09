using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class SettingsDisplay : MonoBehaviour
{
    public TextMeshProUGUI tmp_labels;
    public TextMeshProUGUI tmp_values;

    string[] labels, values;


    public static bool active;

    public GameObject settingsButton;
    public GameObject dvc_angleButton;

    public Color settingsOn, settingsOff;

    void Start()
    {
        labels = new string[3];
        values = new string[3];
        SpawnText();
    }

    void SpawnText()
    {
        labels[0] = "max_dvc_tilt_angle:";
        labels[1] = "align_view_to_dvc:";
        labels[2] = "use_alt_ctrls_scheme:";

        string labelText = "\n";

        for (int i = 0; i < labels.Length; i++) {
            labelText += labels[i] + "\n";
        }
        tmp_labels.text = labelText;

        SetValueText();
    }

    float nextAngTime;
    float nextAngDelay = .125f;

    void SetValueText()
    {
        values[0] = GlobalSettings.GameSettings.max_dvc_tilt_angle.ToString();
        values[1] = GlobalSettings.GameSettings.align_view_to_dvc.ToString();
        values[2] = GlobalSettings.GameSettings.use_alt_ctrl_scheme.ToString();


        string valueText = "\n";
        for(int i = 0; i < values.Length; i++) {
            valueText += values[i] + "\n";
        }

        tmp_values.text = valueText;

    }

    public void InvertActive()
    {
        active = !active;
        SetState();
    }

    public void InvertUseDeviceDirection() {
        if (!active) { return; }
        GlobalSettings.GameSettings.align_view_to_dvc = !GlobalSettings.GameSettings.align_view_to_dvc;
        SetValueText();
    } 

    public void IncrementDeviceAngle()
    {
        if (!active) { return; }

        if (Time.time > nextAngTime)
        {
            InputManager.AdjustDeviceAngle();

            GlobalSettings.GameSettings.max_dvc_tilt_angle = (int)InputManager.DEVICE_TILT_ANGLE;
            GlobalSettings.UpdateSavedValues();

            nextAngTime = Time.time + nextAngDelay;
        }

        SetValueText();
    }

    public void InvertUseAltCtrlScheme() {
        GlobalSettings.GameSettings.use_alt_ctrl_scheme = !GlobalSettings.GameSettings.use_alt_ctrl_scheme;
        GlobalSettings.UpdateSavedValues();
        SetValueText();
    }

    public Vector2 activePos, inactivePos, targetPos;

    void SetState()
    {
        switch (active)
        {
            case true:
                targetPos = activePos;
                settingsButton.GetComponent<UnityEngine.UI.Image>().color = settingsOn;
                break;
            case false:
                targetPos = inactivePos;
                settingsButton.GetComponent<UnityEngine.UI.Image>().color = settingsOff;
                break;
        }
    }
}
