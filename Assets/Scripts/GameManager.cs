using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{

    public static float burnDuration;

    private static GameManager instance;
    public  static GameManager GetInstance()
    {
        return instance;
    }

    public enum GameState { Tutorial, PreBurn, Burn, PostBurn }
    public GameState state = GameState.Tutorial;

    public TutorialController tutorialController;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (this != instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void Start() {
        InitGame();
    }

    bool hasPlayedTutorial = false;

    void InitGame() {
        if (hasPlayedTutorial) {
            SetState(GameState.PreBurn);
        } else {
            hasPlayedTutorial = true;
            SetState(GameState.Tutorial);
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
            case GameState.Tutorial:
                tutorialController.StartTutorial();
                break;                                                              
			case GameState.PostBurn:
				Hand.GetInstance().HandleLevelEnd();
				break;
		}

	}

	void CheckState() {
        switch (state)
        {
            case GameState.Burn:
                burnDuration = Time.time - startTime;           // put this... in a timer script... what are you doing...
                break;
        }
	}

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartBurn() {
        startTime = Time.time;
        SetState(GameState.Burn);
    }

    public void HandleHandDeath() {
        if (state == GameState.Tutorial) {
            tutorialController.HandleHandDeath();
        } else {
            Reset();
        }
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
