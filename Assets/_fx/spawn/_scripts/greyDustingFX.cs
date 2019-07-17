using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class greyDustingFX : MonoBehaviour {

    //GET FADE IN REQ
    [Header("FADE IN: ")]
    public AnimationCurve fadeIn;
    public float timer = 1f;

    //TALK TO WINTER IN LIEU OF FINAL SETUP
    public Winter winter;
    //SHADER PROPS
    int shaderProperty;

    void Start() {

        shaderProperty = Shader.PropertyToID("_cutoff");

    }

    void FixedUpdate() {

        var mainBody = winter.psBody.main;
        mainBody.loop = winter.isMoveing;

        var mainTop = winter.psTop.main;
        mainTop.loop = winter.isMoveing;

        if (winter.isIdle) {

            winter.psBody.Play();
            winter.psTop.Play();
            winter.psBottom.Play();

            Debug.Log("I'm Idle");

        }

        if(winter.isAlert){

            winter.psRing.Play();

            Debug.Log("I'm Alert");

        }


    }
}
