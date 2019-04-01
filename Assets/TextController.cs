using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    // Instruction Sets
    public string[] intro;
    public string[] jumpInstructions;
    public string[] grabInstructions;
    public string[] tiltInstructions;
    int setIndex = 0;

    bool inputReceived = false;


    List<string[]> instructions;

    void Start()
    {
        instructions = new List<string[]>();
        instructions.Add(intro);
        instructions.Add(jumpInstructions);
        instructions.Add(grabInstructions);
        instructions.Add(tiltInstructions);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine(ShowTextSet());
        }

        // if (Input.GetKeyDown(KeyCode.RightShift))
        // {
        //     // IncrementSetIndex();
        // }

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

    public void PlayTextSet(int index)
    {
        setIndex = index;
        StartCoroutine(ShowTextSet());
    }

    public void ReceiveInput()
    {
        inputReceived = true;
    }

    public void GoToNextSet()
    {
        setIndex = (setIndex + 1) % instructions.Count;
    }

    public TextMeshProUGUI tmp;

    public float pauseAfterLines = .125f;
    public float pauseAfterLetters = .0125f;
    public float pauseAfterClears = 1f;

    int lineIndex = 0;
    public bool useNewLines;
    string showString = "";


    public static bool isWaiting;

    IEnumerator ShowTextSet()
    {
        // Debug.Log("set: " + setIndex + "; line: " + lineIndex);

        string[] currentInstructionSet = instructions[setIndex];
        string inputString = currentInstructionSet[lineIndex];
        char[] inputChars = inputString.ToCharArray();

        // if(inputChars[0] == '"') {
        //     Invoke(inputString, 0);
        //     yield return new WaitForEndOfFrame();
        // } 
        // else
        if (inputString == "wait")
        {
            // indefinite waits
            while (!inputReceived)
            {
                isWaiting = true;
                yield return new WaitForEndOfFrame();
            }

            isWaiting = false;
        }
        else
        if (inputString.Substring(0, 5) == ". . .")
        {
            // pauses

            string x = inputString.Substring(5, inputString.Length - 5);
            // Debug.Log("x = " + x);

            float converted = float.Parse(x);

            yield return new WaitForSeconds(converted);
        }
        else
        if (inputString == "/clear")
        {
            // clear

            showString = "";
            tmp.text = showString;

            yield return new WaitForSeconds(pauseAfterClears);
        }
        else
        {
            // linear
            // handle real text
            for (int i = 0; i < inputString.Length; i++)
            {
                showString += inputChars[i];
                tmp.text = showString;
                yield return new WaitForSeconds(pauseAfterLetters);
            }
            yield return new WaitForSeconds(pauseAfterLines);
        }

        lineIndex++;

        if (showString != "")
        {
            // we should only skip lines if the text has not just been cleared
            showString += "\n";
        }

        // When we've exceeded line count, go to the next set and reset line index
        if (lineIndex >= currentInstructionSet.Length)
        {
            setIndex++;
            lineIndex = 0;
        }

        if (setIndex < instructions.Count)
        {
            StartCoroutine(ShowTextSet());
        }

        inputReceived = false;
    }
}
