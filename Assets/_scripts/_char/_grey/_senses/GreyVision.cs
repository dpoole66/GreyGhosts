using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RealisticEyeMovements;

public class GreyVision : MonoBehaviour
{
    [Header("GREY ACCESS")]
    public GameObject grey;
    GreyAwareness aware;
    LookTargetController lookTarget;
    SphereCollider visionScale;
    float visionRange;
    Transform greyEye;
    Transform playerEye;

    //ID CHECK
    bool checkDone;


    private void Start() {

        aware = grey.GetComponent<GreyAwareness>();
        lookTarget = grey.GetComponent<LookTargetController>();
        visionScale = GetComponent<SphereCollider>();
        visionScale.radius = aware.visionScale;
        visionRange = aware.visionRange;
        greyEye = GameObject.FindGameObjectWithTag("greyEye").GetComponent<Transform>();
        playerEye = GameObject.FindGameObjectWithTag("playerEye").GetComponent<Transform>();

    }

    public void VisionOn(){

        StartCoroutine(GreyGaze());
        //StartCoroutine(IDCheck());
        //aware.isSeeing = true;
        //Debug.Log("Vision ON");

    }

    public void VisionOff() {

        //StopCoroutine(IDCheck());
        StopCoroutine(GreyGaze());
        //aware.isSeeing = false;
        //aware.iSeeYou = false;
        //checkDone = false;
        //Debug.Log("Vision OFF");

    }


     public IEnumerator LookAt(){

        lookTarget.LookAtPlayer(-1, 0.075f);
        StartCoroutine(IDCheck());

        yield break;

     }

    public IEnumerator IDCheck(){

          if(!checkDone){

                Debug.Log("Looking for you");

                RaycastHit hit;

                if (Physics.SphereCast(greyEye.transform.position, aware.visionScale, greyEye.forward, out hit, aware.visionRange) && hit.collider.tag == "Player") {


                    Debug.DrawRay(greyEye.transform.position, greyEye.forward, Color.magenta);

                    Debug.Log("I see you " + hit.collider.tag);
                    aware.iSeeYou = true;
                    checkDone = true;


                }

                Debug.Log("Done Looking");
                yield break;

         }

    }

    //GREY GAZE
    public IEnumerator GreyGaze() {

        while (true) {

            Debug.Log("Looking around for things");

            RaycastHit hit;

            if (Physics.SphereCast(greyEye.transform.position, aware.visionScale, greyEye.forward, out hit, aware.visionRange) && hit.collider.tag == "Player") {

                Debug.DrawRay(greyEye.transform.position, greyEye.forward, Color.red);

                Debug.Log("I see you");
                aware.iSeeYou = true;
                //aware.feedback.text = "I see you";
                //checkDone = true;


            } else{

                Debug.DrawRay(greyEye.transform.position, greyEye.forward, Color.blue);

                Debug.Log("I don't see you");
                //aware.feedback.text = "I don't see you.";
                aware.iSeeYou = false;

            }

            yield return null;

        }

    }

}
