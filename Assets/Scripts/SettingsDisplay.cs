using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class SettingsDisplay : MonoBehaviour
{

    // public Dictionary<string, string> stats = new Dictionary<string, string>();
    // TextMeshProUGUI title;
    TextMeshProUGUI[] tmp_labels;
    TextMeshProUGUI[] tmp_values;


    public static bool active;

    public GameObject settingsButton;
    public GameObject dvc_angleButton;

    public Color settingsOn, settingsOff;

    void Start()
    {

        tmp_labels = new TextMeshProUGUI[2];
        tmp_values = new TextMeshProUGUI[2];
        SpawnText();
    }

    void SpawnText() {
    	for (int i = 0; i < tmp_labels.Length; i++) {
    		tmp_labels[i] = new GameObject().AddComponent<TextMeshProUGUI>();
    		tmp_values[i] = new GameObject().AddComponent<TextMeshProUGUI>();

            tmp_labels[i].transform.parent = transform;
            tmp_values[i].transform.parent = transform;
    	}
        tmp_labels[0].text = "align_view_to_dvc:";
        tmp_labels[1].text = "max_dvc_tilt_angle:";


        SetText();
    }

    // void SelectText(int index) {
    // 	for (int i = 0; i < labels.Length; i++) {
    // 		tmp_labels[i].fontStyle = index == i ? FontStyles.Underline : FontStyles.Normal;
    // 		tmp_values[i].fontStyle = index == i ? FontStyles.Underline : FontStyles.Normal;
    // 	}
    // }

    float nextAngTime;
	float nextAngDelay = .125f;

    void SetText() {
        // for(int i = 0; i < tmp_labels.Length; i++) {
            tmp_values[0].text = GlobalSettings.GameSettings.max_dvc_tilt_angle.ToString();
            tmp_values[1].text = GlobalSettings.GameSettings.max_dvc_tilt_angle.ToString();
        // }
    }

    public void InvertActive() {
        active = !active;
        SetState();
    }

    public void IncrementDeviceAngle()
    {
        if(!active) { return; }

        if (Time.time > nextAngTime)
        {
            InputManager.AdjustDeviceAngle();
            GlobalSettings.GameSettings.max_dvc_tilt_angle = (int)InputManager.DEVICE_TILT_ANGLE;
			nextAngTime = Time.time + nextAngDelay;
        }

        SetText();
    }


    public Vector2 activePos, inactivePos, targetPos;

    void SetState() {
        switch(active) {
            case true:
                targetPos = activePos;
                settingsButton.GetComponent<UnityEngine.UI.Image>().color = settingsOn;
                break;
            case false:
                targetPos = inactivePos;
                settingsButton.GetComponent<UnityEngine.UI.Image>().color = settingsOff;
                break;
        }

        // settingsButton .GetComponentInParent(!active);
        // dvc_angleButton.SetActive(active);
    }
    

    
}
