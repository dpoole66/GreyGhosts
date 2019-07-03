using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreyTime : MonoBehaviour {

    [Header("TIMER RUNTIME")]
    public bool countStarted;
    public bool countComplete = true;
    [Header("TIME COUNT")]
    public int countTime;

    //PUBLIC START TIMER METHOD
    public void StartTimer(int countTime){

        StartCoroutine(GreyTimer(countTime));

    }


    //VIOLATION TIMER
    public IEnumerator GreyTimer(int seconds) {

        countTime = seconds;

        while (countTime > 0) {

            countStarted = true;
            countComplete = false;
            yield return new WaitForSeconds(1);
            countTime--;

            if (countTime < 1) {

                countComplete = true;
                countStarted = false;
                sendNotice();
                yield break;

            }

        }  
        
     }

     public void sendNotice(){



     }

}  
