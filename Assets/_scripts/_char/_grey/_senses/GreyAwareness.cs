using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GreyAwareness : MonoBehaviour{

    //RANGEFINDING
    [Header("RANGEFINDING")]
    public float range;
    public float rangeSquareMag;

    //BEING SEEN
    [Header("IS BEING SEEN")]
    public bool isBeingLookedAt;

    //SEEING
    [Header("VISION")]
    [Range(0f, 15f)]
    public float visionRange;
    [Range(0f, 15f)]
    public float visionScale;
    [Header("Final Vision Scale")]
    [Range(1f, 5f)]
    public float visionScaleFactor;
    [Header("IS LOOKING?")]
    public bool isSeeing;
    [Header("IS IN FOV?")]
    public bool inView;
    [Header("IS SEEING PLAYER?")]
    public bool iSeeYou;

    //HEARING
    [Header("HEARING")]
    [Range(0f, 15f)]
    public float hearingRange;
    public bool isHearing = false;

    //PROXIMITY
    [Header("PROXIMITY/COMFORT ZONE")]
    [Range(0f, 15f)]
    public float proximityRange;
    public bool isTooClose;
    public bool isRight;
    public bool isLeft;

    //AWARNESS REQUIRED COMPONENTS
    GameObject grey;
    Transform greyEye;
    Transform playerEye;
    Transform player;
    Animator anim;

    //TRUST
    [Header("TRUST STATUS")]
    public bool notTrusted;

    //VIOLATIONS
    [Header("VIOLATIONS")]
    public int violationCount;

    //TALK TO GREY SENSES
    GreySenses senses;

    //TALK TO GREY VISION
    [Header("TALK TO COMPONENTS")]
    public GreyVision vision;
    public GreySpeech speech;
    public GreyHearing hearing;
    public GreyAnxiety anxiety;
    public GreyTrust trust;
    public GreyExpression expression;
    public GreyReaction reaction;
    public GreyTime timer;

    //IK WORK IN PROGRESS
    [Header("IK RESPONSES")]
    public Transform righthandobj = null;
    public Transform lefthandobj = null;
    [Header("IK SETTINGS")]
    public bool ikActive = false;
    public float rightHandRange = -8f;
    public float leftHandRange = 8f;
    [Header("IK TIMER")]            
    public float timeReact = 0.5f;

    //DEBUG UI
    public Text feedback;
    public Image anxietyMeter;



    private void Start() {

        greyEye = GameObject.FindGameObjectWithTag("greyEye").GetComponent<Transform>();
        playerEye = GameObject.FindGameObjectWithTag("playerEye").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        senses = GetComponent<GreySenses>();
        anim = GetComponent<Animator>();
        //START RESPONSES
        expression.Response();
       

    }

    private void FixedUpdate() {

        if (Input.GetKeyDown(KeyCode.T)) {

            timer.StartTimer(8);

        }

        //RANGEFINDING
        range = Vector3.Distance(greyEye.transform.position, playerEye.transform.position);
        rangeSquareMag = (greyEye.transform.position - playerEye.transform.position).sqrMagnitude;

        //HEARING
        isHearing = range < hearingRange;

        if (isHearing) {

            //hearing.HearingOn();

        }

        //VISION
        visionScale = range / visionScaleFactor;

        //SEEING STATE FOV CHECK
        Vector3 forward = greyEye.TransformDirection(Vector3.forward);
        Vector3 toTarget = playerEye.transform.position - greyEye.transform.position;
        toTarget.y = 0f;

        //NOTE TO SELF, 6/27/19 EDITS TO ADD FULL VISION 
        //if (Vector3.Dot(forward, toTarget) > 0.75f) {

        //    //TARGET IS IN FOV
        //    inView = true;
        //    vision.VisionOn();

        //} else {

        //    inView = false;
        //    vision.VisionOff();

        //}

        if (Input.GetKey(KeyCode.V)) {

            vision.VisionOn();

        }

        if (Input.GetKey(KeyCode.O)) {

            vision.VisionOff();

        }

        if (Input.GetKey(KeyCode.A)) {

            this.transform.Rotate(0f, -120f * Time.deltaTime, 0f);
            anim.SetBool("turnLeft", true);

        }

        if (Input.GetKeyUp(KeyCode.A)) {

            anim.SetBool("turnLeft", false);

        }



        if (Input.GetKey(KeyCode.D)) {

            this.transform.Rotate(0f, 120f * Time.deltaTime, 0f);
            anim.SetBool("turnRight", true);

        }

        if (Input.GetKeyUp(KeyCode.D)) {

            anim.SetBool("turnRight", false);

        }


        //ANXIETY
        if (range <= proximityRange) {


            float anxietyScale = range / proximityRange;
            float anxietyFactor = anxietyScale * 100f;

            anxiety.anxiety = 100f - anxietyFactor;

            //Debug.Log("Scale: " + anxietyScale);
            //Debug.Log("Factor: " + anxietyFactor);
            //Debug.Log("Anxiety: " + anxiety.anxiety);

            feedback.text = anxiety.anxiety.ToString();
            anxietyMeter.fillAmount = anxiety.anxiety / 100f;

        } else {

            anxiety.anxiety = 0f;
            feedback.text = anxiety.anxiety.ToString();

        }

        //VIOLATION AND TRUST   
        isTooClose = range <= proximityRange;

        violationCount = trust.violations;
        notTrusted = trust.notTrusted;

        if(isTooClose && !trust.notTrusted) {

            trust.loseTrust();

        }

        if (!isTooClose && trust.notTrusted) {

            trust.gainTrust();
            Debug.Log("Gain Trust");

        }

        //EXPRESSION RESPONSE
        expression.Response();

        //PHUYSICAL RESPONSE
        ikActive = isTooClose;

        if (isTooClose) {

            Vector3 targetDir = player.transform.position - transform.position;
            Vector3 forwardDir = transform.forward;


            float angle = Vector3.SignedAngle(targetDir, forward, Vector3.up);

            reaction.right = angle < rightHandRange;
            reaction.left = angle > leftHandRange;
            reaction.steppingBack = true;


            isRight = angle < -5f;
            isLeft = angle > 5f;

            if (angle < -5f) {

                reaction.left = false;
                reaction.right = true;
                //Debug.Log("Grab Right");


            } else if (angle > 5f) {

                reaction.right = false;
                reaction.left = true;
                //Debug.Log("Grab Left");

            }

        }   else{

            reaction.steppingBack = false;

        }

    }

    private void OnAnimatorIK() {

        //Timer = Timer -= Time.deltaTime * 1f;

        //if (Timer <= 0.0f) {

        //    Timer = 3.0f;
        //    Debug.Log("Timer: " + Timer);

        //}



        if (ikActive && isRight) {

            //REACH RIGHT

            Debug.Log("IK Grab Right");

            //SET HAND FLOAT
            anim.SetFloat("rhand", 1, timeReact, Time.deltaTime * 1f);
            anim.SetBool("right", true);

            //USE HAND FOR POS WEIGHT
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, anim.GetFloat("rhand"));
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, anim.GetFloat("rhand"));

            //SET HAND POSITION
            anim.SetIKPosition(AvatarIKGoal.RightHand, righthandobj.position);
            anim.SetIKRotation(AvatarIKGoal.RightHand, righthandobj.rotation);

        } else {

            //SET HAND FLOAT               
            anim.SetFloat("rhand", 0, timeReact, Time.deltaTime * 1f);
            anim.SetBool("right", false);

            //USE HAND FOR POS WEIGHT
            //Timer = 1.0f;
            anim.SetFloat("timer", timer.countTime);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, anim.GetFloat("rhand"));
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, anim.GetFloat("rhand"));

            //SET HAND POSITION
            anim.SetIKPosition(AvatarIKGoal.RightHand, righthandobj.position);
            anim.SetIKRotation(AvatarIKGoal.RightHand, righthandobj.rotation);


        }

        if (ikActive && isLeft) {

            Debug.Log("IK Grab Left");

            //SET HAND FLOAT
            anim.SetFloat("lhand", 1, timeReact, Time.deltaTime * 1f);
            anim.SetBool("left", true);

            //USE HAND FOR POS WEIGHT
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, anim.GetFloat("lhand"));
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, anim.GetFloat("lhand"));

            //SET HAND POSITION
            anim.SetIKPosition(AvatarIKGoal.LeftHand, lefthandobj.position);
            anim.SetIKRotation(AvatarIKGoal.LeftHand, lefthandobj.rotation);

        } else {

            //SET HAND FLOAT               
            anim.SetFloat("lhand", 0, timeReact, Time.deltaTime * 1f);
            anim.SetBool("left", false);

            //USE HAND FOR POS WEIGHT
            //Timer = 1.0f;
            anim.SetFloat("timer", timer.countTime);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, anim.GetFloat("lhand"));
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, anim.GetFloat("lhand"));

            //SET HAND POSITION
            anim.SetIKPosition(AvatarIKGoal.LeftHand, lefthandobj.position);
            anim.SetIKRotation(AvatarIKGoal.LeftHand, lefthandobj.rotation);


        }

    }

    //private void OnAnimatorIK() {

    //    anim.SetBool("right", isRight);
    //    anim.SetBool("left", isLeft);

    //    if (isRight) {

    //        anim.SetFloat("rhand", 1, 0.5f, Time.deltaTime * 0.3f);
    //        //USE HAND FOR POS WEIGHT
    //        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, anim.GetFloat("rhand"));
    //        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, anim.GetFloat("rhand"));

    //        //SET HAND POSITION
    //        anim.SetIKPosition(AvatarIKGoal.RightHand, playerEye.position);
    //        anim.SetIKRotation(AvatarIKGoal.RightHand, playerEye.rotation);

    //    }

    //}

}
