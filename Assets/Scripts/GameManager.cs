using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static float burnDuration;

    private static GameManager instance;
    public static GameManager GetInstance()
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
				Hand.GetInstance().HandleLevelEnd();
				break;
		}

	}

	void CheckState() {
        switch (state)
        {
            case GameState.Burn:
                burnDuration = Time.time - startTime;
                break;
        }
	}

    public void StartBurn() {
        startTime = Time.time;
        SetState(GameState.Burn);
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void HandleLevelEnd()
    {
        EndTimer();
		SetState(GameState.PostBurn);
    }

    float startTime, endTime;

    public void EndTimer()
    {
        endTime = Time.time;
    }
}
