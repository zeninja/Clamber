using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ReGameManager : MonoBehaviour
{

    private static ReGameManager instance;
    public static ReGameManager GetInstance()
    {
        return instance;
    }

    public enum GameState { PreBurn, Burn, PostBurn }
    GameState state = GameState.PreBurn;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (this != instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) || Input.touchCount == 3)
        {
            Reset();
        }

		CheckState();
    }

	void SetState(GameState newState) {
		state = newState;

		switch(state) {
			case GameState.PostBurn:
				
				break;
		}

	}

	void CheckState() {
        switch (state)
        {
            case GameState.Burn:
                float burnDuration = Time.time - startTime;
                timer.text = burnDuration.ToString("F2");
                break;
        }
	}

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void HandleLevelEnd()
    {
        EndTimer();
		SetState(GameState.PostBurn);
        // Reset();
    }

    float burnDuration;
    int holdsSkipped;

    float startTime, endTime;

    public TextMeshProUGUI timer;

    public void StartGame()
    {
		if(state == GameState.PreBurn) {
			startTime = Time.time;
			SetState(GameState.Burn);
		}
    }

    public void EndTimer()
    {
        endTime = Time.time;
    }
}
