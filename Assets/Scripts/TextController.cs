using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    [HideInInspector]
    public TutorialController tutorialController;
    TextMeshProUGUI tapToContinue;

    public string[] text;

    [HideInInspector]
    public int setIndex = 0;

    bool inputReceived = false;

    void Start()
    {
        tapToContinue = tutorialController.tapToContinue;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine(ShowTextSet());
        }

        CheckForInput();
    }

    void CheckForInput()
    {
        if (InputManager.inputJumpStart)
        {
            inputReceived = true;
        }
    }

    public void PlayTextSet()
    {
        StartCoroutine(ShowTextSet());
    }

    public TextMeshProUGUI tmp;
    public float pauseAfterLines = .125f;
    public float pauseAfterLetters = .0125f;
    public float pauseAfterClears = 1f;
    int lineIndex = 0;
    string showString = "";
    public bool isWaiting;

    public void Stop()
    {
        StopAllCoroutines();
    }

    IEnumerator ShowTextSet()
    {
        // Debug.Log("showing text set: " + setIndex);
        string[] currentInstructionSet = text;
        string inputString = currentInstructionSet[lineIndex];
        char[] inputChars = inputString.ToCharArray();

        if (inputChars[0] == '/')
        {
            // "command"
            yield return StartCoroutine(ProcessCommand(inputString));
        }
        else
        {
            // real text
            for (int i = 0; i < inputString.Length; i++)
            {
                showString += inputChars[i];
                tmp.text = showString;
                yield return new WaitForSeconds(pauseAfterLetters);
            }

            SkipLine();
        }
        yield return new WaitForSeconds(pauseAfterLines);
        HandleLineCompleted(currentInstructionSet.Length);

        inputReceived = false;
    }

    IEnumerator ProcessCommand(string inputString)
    {
        switch (inputString)
        {
            case "/space":
                SkipLine();
                yield return null;
                yield break;

            case "/wait":
                float t = 0;
                while (!inputReceived)
                {
                    // Debug.Log("waiting");
                    isWaiting = true;
                    t += Time.deltaTime;

                    if (t > 1)
                    {
                        tapToContinue.gameObject.SetActive(true);
                    }
                    yield return new WaitForEndOfFrame();
                }
                tapToContinue.gameObject.SetActive(false);
                isWaiting = false;

                SkipLine();

                yield break;

            case "/hang":
                SkipLine();
                yield return new WaitForSeconds(.1f);
                yield break;

            case "/clear":
                showString = "";
                tmp.text = showString;
                yield return new WaitForSeconds(pauseAfterClears);
                yield break;

            case "/enableJump":
                tutorialController.EnableJump();
                yield break;
            case "/enableGrab":
                tutorialController.EnableGrab();
                yield break;
            case "/enableHolds":
                tutorialController.EnableHolds();
                yield break;
            case "/enableAllHolds":
                tutorialController.EnableAllHolds();
                yield break;
            // case "/enableTilt":
            //     tutorialController.EnableTilt();
            //     yield break;
            // case "/enableCamera":
            //     tutorialController.EnableCamera();
            //     yield break;
            case "/prepGrab":
                tutorialController.PrepGrab();
                yield break;
            case "/enableTracking":
                tutorialController.EnableTracking();
                yield break;
            case "/resetDeath":
                tutorialController.Reset();
                yield break;
            case "/confirmGrab":
                tutorialController.ConfirmGrab();
                yield break;
            // case "/waitForGyro":
            //     tutorialController.WaitForGyro();
            //     yield break;
            // case "/endTutorial":  
        }
    }

    void SkipLine() {
        showString += "\n";
    }


    void HandleLineCompleted(int setLength)
    {
        lineIndex++;

        if (lineIndex >= setLength)
        {
            lineIndex = 0;

            if (tutorialController != null)
            {
                // Debug.Log("GOING BACK TO TUTORIAL: " + "\n" + "set: " + setIndex + "; line: " + lineIndex);
                // tutorialController.PlayTutorialText(setIndex);
                tutorialController.HandleSetCompleted(setIndex);
            }
        }
        else
        {
            StartCoroutine(ShowTextSet());
        }
    }
}
