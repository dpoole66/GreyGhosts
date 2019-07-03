using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blending : MonoBehaviour {

    int blendShapeCount;
    [Header("ACTION BOOLS")]
    public bool isFrowning = false;
    float runningFrownRate, runningBrowsRate, runningSquintRate;
    //PERSONAL SPACE FOR TRIGGERS
    [Header("PERSONAL SPACE ACTIONS")]
    public PersonalSpace personalSpace;
    //SkinnedMeshRenderer skinrend;
    //Mesh skinmesh;
    //GET SKINNED MESH RENDERERS FOR BODY PARTS
    //BODY
    SkinnedMeshRenderer bodyskinrend;
    Mesh bodyskinmesh;
    //EYELASHES
    SkinnedMeshRenderer eyelashskinrend;
    Mesh eyelashskinmesh;
    //BLEND SETTINGS
    [Header ("MOUTH BLEND SETTINGS")]
    public float frownRate= 10f;
    public float frownMax = 50f;        
    [Header("BROWS BLEND SETTINGS")]
    public float browsRate = 10f;
    public float browsMax = 40f;
    [Header("EYES BLEND SETTINGS")]
    public float squintRate = 0f;
    public float squintMax = 20f;
    [Header("BLEND SPEED")]
    float blendSpeed = 10f;
    bool frownFinished = false;
    
   

    private void Awake() {

        //skinrend = GetComponent<SkinnedMeshRenderer>();
        //skinmesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;
        bodyskinrend = GameObject.FindGameObjectWithTag("ikBody").GetComponent<SkinnedMeshRenderer>();
        bodyskinmesh  = GameObject.FindGameObjectWithTag("ikBody").GetComponent<SkinnedMeshRenderer>().sharedMesh;
        eyelashskinrend = GameObject.FindGameObjectWithTag("ikEyeLashes").GetComponent<SkinnedMeshRenderer>();
        eyelashskinmesh = GameObject.FindGameObjectWithTag("ikEyeLashes").GetComponent<SkinnedMeshRenderer>().sharedMesh;


    }

    private void Start() {

        blendShapeCount = bodyskinmesh.blendShapeCount;
        runningFrownRate = frownRate;
        runningBrowsRate = browsRate;
        runningSquintRate = squintRate;

    }

    private void Update() {

        isFrowning = personalSpace.ikActive;

        //Debug.Log(frownFinished);
        if (isFrowning) {

            StartCoroutine(Frown());
            //if (frownRate<= 100f) {
            //    Debug.Log("Frown");
            //    //BROWS
            //    skinrend.SetBlendShapeWeight(2, brows);
            //    skinrend.SetBlendShapeWeight(3, brows);
            //    //FROWN
            //    skinrend.SetBlendShapeWeight(14, frown);
            //    skinrend.SetBlendShapeWeight(15, frown);
            //    //SQUINT
            //    skinrend.SetBlendShapeWeight(43, squint);
            //    skinrend.SetBlendShapeWeight(44, squint);
            //    //SPEED
            //    frownRate+= blendSpeed;
            //    brows += blendSpeed;
            //    squint += blendSpeed;

            //}


        } else if (!isFrowning) {

            StartCoroutine(Calm());
            //if (frownRate> 0f) {
            //    Debug.Log("Calm");
                ////BROWS
                //skinrend.SetBlendShapeWeight(2, frown);
                //skinrend.SetBlendShapeWeight(3, frown);
                ////FROWN;
                //skinrend.SetBlendShapeWeight(14, frown);
                //skinrend.SetBlendShapeWeight(15, frown);
                ////SQUINT
                //skinrend.SetBlendShapeWeight(44, squint);
                //skinrend.SetBlendShapeWeight(45, squint);
                ////SPEED
                //frownRate-= blendSpeed;
                //brows -= blendSpeed;
                //squint -= blendSpeed;
            //}

        }
    }

    IEnumerator Frown() {
    
        if (runningFrownRate < frownMax) {
            Debug.Log("Coroutine Frown");
            //BROWS
            bodyskinrend.SetBlendShapeWeight(2, browsMax);
            bodyskinrend.SetBlendShapeWeight(3, browsMax);
            //FROWN
            bodyskinrend.SetBlendShapeWeight(14, frownMax);
            bodyskinrend.SetBlendShapeWeight(15, frownMax);
            //SQUINT
            bodyskinrend.SetBlendShapeWeight(43, squintMax);           //LEFT EYE
            bodyskinrend.SetBlendShapeWeight(44, squintMax);           //RIGHT EYE
            eyelashskinrend.SetBlendShapeWeight(43, squintMax);      //LEFT EYELASH
            eyelashskinrend.SetBlendShapeWeight(44, squintMax);      //RIGHT EYELASH
            //SPEED
            runningFrownRate += blendSpeed;
            runningBrowsRate += blendSpeed;
            runningSquintRate += blendSpeed;

            yield return null;

        }

        yield break;
    }

    IEnumerator Calm() {

        if (runningFrownRate == frownMax) {
            Debug.Log("Coroutine Calm");
            //BROWS
            bodyskinrend.SetBlendShapeWeight(2, 0);
            bodyskinrend.SetBlendShapeWeight(3, 0);
            //FROWN;
            bodyskinrend.SetBlendShapeWeight(14, 0);
            bodyskinrend.SetBlendShapeWeight(15, 0);
            //SQUINT
            bodyskinrend.SetBlendShapeWeight(43, 0);
            bodyskinrend.SetBlendShapeWeight(44, 0);
            //SPEED
            runningFrownRate -= blendSpeed;
            runningBrowsRate -= blendSpeed;
            runningSquintRate -= blendSpeed;

            yield return null;

        }
        runningFrownRate = frownRate;
        runningBrowsRate = browsRate;
        runningSquintRate = squintRate;
        yield break;
    }

}
