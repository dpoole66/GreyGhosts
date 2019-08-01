using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class Lerper : MonoBehaviour{

    [Header("Grey Status: ")]
    public GreyStatus stats;

    [SerializeField]
    private float start = 0;
    [SerializeField]
    private float end = 100;

    [SerializeField]
    [Range(0f, 1f)]
    public float lerpOutput = 0.5f;

    private float t = 0f;

    //SET TIME AMOUT
    public float lerpTime;
    public bool isLerping;

    private void Update() {

        //lerpOutput = Mathf.Lerp(start, end, lerpPct);
        if(Input.GetKeyDown(KeyCode.Space)){

            //isLerping = true;
            StartCoroutine(Lerp(start, end, lerpTime));

        }

   
    }

    public void LerpThis(){

        StartCoroutine(Lerp(0f, 1f, lerpTime));

    }


    IEnumerator Lerp(float start, float end, float duration) {

        while (true) {

            isLerping = true;

            //Debug.Log("Lerper" + "    Start:  " + start + "    End:  " + end + "    Duration:  " + duration + "    Is Lerping?  " + isLerping + "    t Time:   " + t + "   LerpOutput:    " + lerpOutput);

            t += Time.deltaTime;
            duration = lerpTime;

            //INTERNAL
            float blend = Mathf.Clamp01(t / duration);
            lerpOutput = Mathf.Lerp(start, end, blend);

            if (lerpOutput >= end) {

                //Debug.Log("Break Lerper:  " + lerpOutput);
                t = 0f;
                isLerping = false;

                yield break;

            }

            yield return null;

        }


    }

}
