using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreyTrust : MonoBehaviour
{
    [Header("GREY COMPONENTS")]
    public GreyAwareness aware;
    public GreyAnxiety anxiety;
    //public GreyTime timer;

    [Header("FRIEND OR FOE")]
    public bool isFriend;
    public bool notTrusted;

    [Header("TRUST TIME")]
    public int countTime;
    public int countDown;
    public bool countStarted;
    public bool countComplete;

    [Header("VIOLATIONS")]
    public int violations;

    public void loseTrust(){

        if (!countStarted) {

            StartCoroutine(LoseTrust(countTime));

        }

    }

    public void gainTrust() {

        if (!countStarted) {

            StartCoroutine(GainTrust(countTime));

        }

    }


    public IEnumerator LoseTrust(int seconds){

        countDown = seconds;

        while (seconds > 0) {

            countStarted = true;
            countComplete = false;
            yield return new WaitForSeconds(1);
            countDown--;
            seconds--;

            if (seconds < 1) {

                countComplete = true;
                countStarted = false;

                violations++;
                notTrusted = true;

                yield break;

            }

        }

    }

    public IEnumerator GainTrust(int seconds) {

        countDown = seconds;

        while (seconds > 0) {

            countStarted = true;
            countComplete = false;
            yield return new WaitForSeconds(1);
            countDown--;
            seconds--;

            if (seconds < 1) {

                countComplete = true;
                countStarted = false;

                violations--;
                notTrusted = false;

                yield break;

            }

        }

    }

}
