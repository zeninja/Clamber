using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{

    public string[] instructions;

    bool inputReceived = false;

    int instructionIndex = 0;

    public void IncrementIndex() {
        instructionIndex = (instructionIndex + 1) % instructions.Length;
    }

    TextMeshProUGUI tmp;
    public float timeBetweenLetters = .0125f;

    IEnumerator ShowText() {
        string targetString = instructions[instructionIndex];
        char[] chars = targetString.ToCharArray();
        string showString = "";

        for(int i = 0; i < targetString.Length; i++) 
        {
            showString += chars[i];
            tmp.text = showString;
            yield return new WaitForSeconds(timeBetweenLetters);
        }
    }
}
