using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
	[System.Serializable]
    public struct GameSettings
    {
        public static bool align_view_to_dvc = true;
        public static int max_dvc_tilt_angle = 15;
    }

    [SerializeField]
    public static GameSettings Settings;
    static string jsonString;

    void Awake()
    {
        if (PlayerPrefs.HasKey("SAVED"))
        {
            jsonString = PlayerPrefs.GetString("JSON");
            UpdateInGameValues();
        }
        else
        {
            InitValues();
        }
    }

    void InitValues()
    {
        Settings = new GameSettings();
		
        UpdateSavedValues();
    }

    void UpdateInGameValues()
    {
        Settings = JsonUtility.FromJson<GameSettings>(jsonString);

        UpdateSavedValues();
    }

	public static void UpdateSavedValues() {
		jsonString = JsonUtility.ToJson(Settings);
        PlayerPrefs.SetString("JSON", jsonString);
        PlayerPrefs.SetInt("SAVED", 1);
		PlayerPrefs.Save();
	}
}
