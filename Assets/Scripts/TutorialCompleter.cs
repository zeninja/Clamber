using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCompleter : MonoBehaviour
{
    public TutorialController tc;


    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            tc.HandleTutorialComplete();
        }
    }

}
