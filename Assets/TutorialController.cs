using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialController : MonoBehaviour
{
    public TextMeshProUGUI labels, values;
    public float completion = 0;

    string falseDash = "-----------------";
    string trueDash = "------------------";
    string dash;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartTutorial();
        }
    }

    public enum TutorialState { off, teachJump, teachGrab, teachAim };
    TutorialState state = TutorialState.off;

    void StartTutorial()
    {
        Hand.GetInstance().canJump = false;
        Hand.GetInstance().canGrab = false;
        Hand.GetInstance().canTilt = false;

        jumpText.SetActive(false);
        grabText.SetActive(false);
        tiltText.SetActive(false);

        SetTutorialState(TutorialState.teachJump);
        StartCoroutine(Tutorial());
    }

    public GameObject jumpText;
    public GameObject grabText;
    public GameObject tiltText;

    IEnumerator Tutorial()
    {
        while (completion < 1)
        {
            switch (state)
            {
                case TutorialState.teachJump:
                    jumpText.SetActive(true);
                    break;
                case TutorialState.teachGrab:
                    jumpText.SetActive(false);
                    grabText.SetActive(true);
                    break;
                case TutorialState.teachAim:
                    grabText.SetActive(false);
                    tiltText.SetActive(true);
                    break;
            }
            SetText();

            yield return null;
        }
    }


    public void SetTutorialState(TutorialState newState)
    {
        state = newState;
        switch (state)
        {
            case TutorialState.teachJump:
                Hand.GetInstance().canJump = true;
                break;
            case TutorialState.teachGrab:
                Hand.GetInstance().canGrab = true;
                break;
            case TutorialState.teachAim:
                Hand.GetInstance().canTilt = true;
                break;
        }
    }


    void SetText()
    {
        bool canJump = Hand.GetInstance().canJump;
        bool canGrab = Hand.GetInstance().canGrab;
        bool canTilt = Hand.GetInstance().canTilt;

        string dash1 = canJump ? trueDash : falseDash;
        string dash2 = canGrab ? trueDash : falseDash;
        string dash3 = canTilt ? trueDash : falseDash;

        labels.text = "\n" + // skip first line for title
                      "JUMP" + dash1 + "\n" +
                      "GRAB" + dash2 + "\n" +
                      "AIM" + dash3 + "-";

        string jumpString = canJump ? "[TRUE]" : "[FALSE]";
        string grabString = canGrab ? "[TRUE]" : "[FALSE]";
        string tiltString = canTilt ? "[TRUE]" : "[FALSE]";

        values.text = "\n" + // skip first line for title
                    jumpString + "\n" +
                    grabString + "\n" +
                    tiltString + "\n";
    }


}
