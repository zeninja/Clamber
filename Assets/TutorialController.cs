using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialController : MonoBehaviour
{

    public TextMeshProUGUI labels, values, tapToContinue;
    // string falseDash = "-----------------";
    // string trueDash = "------------------";
    // string dash;

    TextController[] textControllers;

    void Awake()
    {
        textControllers = GetComponentsInChildren<TextController>();

        int i = 0;
        foreach (TextController tc in textControllers)
        {
            tc.tutorialController = this;
            tc.setIndex = i;
            i++;
        }

        // DontDestroyOnLoad(gameObject);
    }

    public void StartTutorial()
    {
        // Debug.Log("starting!!!");
        if (!started)
        {
            started = true;
            // Debug.Log("staraaaaaaaaaaaaaaaaaaaaaaating!!!");
            Hand.GetInstance().PrepForTutorial();
            state = TutorialState.Intro;
            DoTutorial();
        }
    }

    bool started = false;
    bool hasTaughtJump = false;

    public enum TutorialState { Intro, TeachJump, JumpConfirmed, TeachGrab, ActivateCamera, ActivateGyro, TutorialComplete };
    public TutorialState state = TutorialState.Intro;

    void DoTutorial()
    {
        Debug.Log("Doing " + state);
        textControllers[(int)state].PlayTextSet();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            StopAllCoroutines();
            StopAllText();
            GoToNextTutorialSegment();
        }
    }

    public GameObject handPrefab;

    public void Reset()
    {
        // Debug.Log("Write code to Reset the tutorial now");
        state = (TutorialState)deathIndex;
        Hand.GetInstance().Reset();
        textControllers[(int)state].PlayTextSet();
        // Instantiate(handPrefab);
    }


    void StopAllText()
    {
        foreach (TextController tc in textControllers)
        {
            tc.Stop();
        }
    }

    public void HandleSetCompleted(int lastPlayedIndex)
    {
        // text is done playing on screen, do whatever else needs to be done...
        // Debug.Log("Set " + lastPlayedIndex + " completed");

        switch (lastPlayedIndex)
        {
            case (int)TutorialState.Intro:
                GoToNextTutorialSegment();
                break;
            case (int)TutorialState.TeachJump:
                // Jump introduced
                // Confirm jump instead of immediately going to next state
                break;
            case (int)TutorialState.JumpConfirmed:
                // jump confirmed
                GoToNextTutorialSegment();
                break;
            case (int)TutorialState.TeachGrab:
                // grab introduced
                // Confirm grab instead of immediately going to next state
                break;
            // case (int)TutorialState.GrabConfirmed:
            //     // tracking
            //     GoToNextTutorialSegment();
            //     break;
            case (int)TutorialState.ActivateCamera:
                // GoToNextTutorialSegment();
                break;
            case (int)TutorialState.ActivateGyro:
                // GoToNextTutorialSegment();
                break;
            case (int)TutorialState.TutorialComplete:
                Debug.Log("tutorialComplete");
                break;
        }
    }

    void GoToNextTutorialSegment()
    {
        state++;
        DoTutorial();
    }

    int textIndex;

    void PlayText()
    {
        textControllers[textIndex].PlayTextSet();
    }

    void PlayNextTextSet()
    {
        textIndex++;
        textControllers[textIndex].PlayTextSet();
    }

    int deathIndex;

    public void HandleHandDeath()
    {
        deathIndex = (int)state;

        if (deathIndex == (int)TutorialState.TeachJump)
        {
            Debug.Log("Jump confirmed. Playing text");
            ConfirmJump();
        }
        else
        {
            StopAllCoroutines();
            StopAllText();
            textControllers[textControllers.Length - 1].PlayTextSet();
        }
    }

    void ConfirmJump()
    {
        // state = TutorialState.JumpConfirmed;
        // textControllers[(int)TutorialState.JumpConfirmed].PlayTextSet();
        GoToNextTutorialSegment();
    }

    public void EnableJump()
    {
        Hand.GetInstance().canJump = true;

    }

    public void EnableGrab()
    {
        Hand.GetInstance().canGrab = true;
    }

    public GameObject tutorialHolds;

    public void EnableHolds()
    {
        tutorialHolds.SetActive(true);
    }

    public void EnableAllHolds()
    {
        // hacky
        tutorialHolds.GetComponent<MaskedDots>().EnableAllHolds();
    }

    public void EnableGyro() {
        if(!Hand.GetInstance().canTilt) {
            Hand.GetInstance().canTilt = true;
            textControllers[(int)TutorialState.ActivateGyro].PlayTextSet();
        }
    }

    // public void DisableHolds()
    // {
    //     tutorialHolds.SetActive(true);
    // }

    // public void EnableCamera()
    // {
    //     Debug.Log("SUPPOSED TO BE ENABLING THE CAMERA BUT NOT ACTUALLY DOING ANYTHING YET.");
    // }

    public void PrepGrab()
    {
        Hand.GetInstance().Reset();
    }



    public void ConfirmGrab()
    {
        StartCoroutine(WaitForGrab());
    }

    bool playerHasGrabbed = false;

    IEnumerator WaitForGrab()
    {
        while (!playerHasGrabbed)
        {
            if (Hand.OnHold())
            {
                grabCount++;

                if (grabCount > 3)
                {
                    playerHasGrabbed = true;
                    GoToNextTutorialSegment();
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }

    int grabCount = 0;
    public CameraFollow cameraTracking;

    public void EnableTracking()
    {
        cameraTracking.trackX = true;
        cameraTracking.trackY = true;
    }
}
