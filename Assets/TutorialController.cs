using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialController : MonoBehaviour
{
    public TextMeshProUGUI labels, values, tapToContinue;
    // public float completion = 0;

    string falseDash = "-----------------";
    string trueDash = "------------------";
    // string dash;

    TextController textController;

    void Start() {
        textController = GetComponent<TextController>();

        // StartTutorial();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartTutorial();
        }

        ProcessInput();

        CheckWait();
    }

    bool started = false;

    void ProcessInput() {
        if (InputManager.inputJumpStart && !started) {
            started = true;
            StartTutorial();
        }
    }

    public enum TutorialState { off, intro, teachJump, teachGrab, teachAim };
    TutorialState state = TutorialState.off;

    void StartTutorial()
    {
        Hand.GetInstance().canJump = false;
        Hand.GetInstance().canGrab = false;
        Hand.GetInstance().canTilt = false;

        textController.PlayTextSet(0);
    }

    bool startedWaitTimer;
    float waitTimer = 1f;
    float waitStartTime;

    void CheckWait() {
        float timeElapsed = 0;

        if (TextController.isWaiting) {
            if(!startedWaitTimer) {
                waitStartTime = Time.time;
                startedWaitTimer = true;
            } else {
                timeElapsed = Time.time - waitStartTime;

                if(timeElapsed > waitTimer) {
                    tapToContinue.gameObject.SetActive(true);
                }
            }
        } else {
            tapToContinue.gameObject.SetActive(false);
        }
    }


    // public GameObject jumpText;
    // public GameObject grabText;
    // public GameObject tiltText;

    // IEnumerator Tutorial()
    // {
    //     while (completion < 1)
    //     {
    //         switch (state)
    //         {
    //             case TutorialState.teachJump:
    //                 break;
    //             case TutorialState.teachGrab:
    //                 break;
    //             case TutorialState.teachAim:
    //                 break;
    //         }
    //         SetText();

    //         yield return null;
    //     }
    // }

    // public void SetTutorialState(TutorialState newState)
    // {
    //     state = newState;
    //     switch (state)
    //     {
    //         case TutorialState.teachJump:
    //             Hand.GetInstance().canJump = true;
    //             break;
    //         case TutorialState.teachGrab:
    //             Hand.GetInstance().canGrab = true;
    //             break;
    //         case TutorialState.teachAim:
    //             Hand.GetInstance().canTilt = true;
    //             break;
    //     }
    // }

    // void SetText()
    // {
    //     bool canJump = Hand.GetInstance().canJump;
    //     bool canGrab = Hand.GetInstance().canGrab;
    //     bool canTilt = Hand.GetInstance().canTilt;

    //     string dash1 = canJump ? trueDash : falseDash;
    //     string dash2 = canGrab ? trueDash : falseDash;
    //     string dash3 = canTilt ? trueDash : falseDash;

    //     labels.text = "\n" + // skip first line for title
    //                   "JUMP" + dash1 + "\n" +
    //                   "GRAB" + dash2 + "\n" +
    //                   "AIM" + dash3 + "-";

    //     string jumpString = canJump ? "[TRUE]" : "[FALSE]";
    //     string grabString = canGrab ? "[TRUE]" : "[FALSE]";
    //     string tiltString = canTilt ? "[TRUE]" : "[FALSE]";

    //     values.text = "\n" + // skip first line for title
    //                 jumpString + "\n" +
    //                 grabString + "\n" +
    //                 tiltString + "\n";
    // }

}
