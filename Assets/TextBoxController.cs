using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class TextBoxController : MonoBehaviour
{
    TextMeshProUGUI tmp;
    
    
    string inputString;
    string showString = "";

    public float pauseAfterLetters = .0125f;

    // public bool showOnStart = true;

    void Start() {
        tmp = GetComponent<TextMeshProUGUI>();
        inputString = tmp.text;

        gameObject.SetActive(false);
        // if(showOnStart) {
        //     gameObject.SetActive(true);
        // }
    }

    void OnEnable()
    {
        if(inputString == null || tmp == null) { return; }
        StartCoroutine(PlayText());
    }

    public IEnumerator PlayText() {
        // Debug.Log(inputString);
        // Debug.Break();
        char[] chars = inputString.ToCharArray();

        float t = 0;
        float d = .25f;

        while(t < d) {
            float p = t / d;
            int sub = (int)((EZEasings.SmoothStart5(p) * inputString.Length) + 1);

            showString = inputString.Substring(0, sub);

            // Debug.Log(tmp);
            // Debug.Log(showString);

            tmp.text   = showString;

            t += Time.fixedDeltaTime;
            yield return new WaitForEndOfFrame();
        }


        // for (int i = 0; i < inputString.Length; i++)
        // {
        //     showString += chars[i];
        //     tmp.text = showString;
        //     // yield return new WaitForSeconds(pauseAfterLetters);
        // }
    }

    public void HideText() {
        showString = "";
        tmp.text = showString;
    }
}
