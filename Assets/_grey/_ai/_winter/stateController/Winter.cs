using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Winter : MonoBehaviour {
    //STATE AND FEEDBACK: 
    [Header("Current State Update: ")]
    public State currentState;
    public State remainState;

    //REQUIRED COMPONENTS:
    [Header("Required Components: ")]
    public GreyStatus status;
    public Transform greyEyes;

    [Header("Timer: ")]
    public float countdown;
    public float startTime;
    public bool timerStarted;
    public bool timerEnded;

    [Header("REM: ")]
    public RealisticEyeMovements.LookTargetController lookTargeting;

    [Header("Get Player: ")]
    public Transform player;

    ////FADEING
    //[Header("Meshes to Fade: ")]
    //public SkinnedMeshRenderer[] fadeRenderers = new SkinnedMeshRenderer[7];
    ////public SkinnedMeshRenderer fader;

    ////FADE TIMER AND BOOL
    //[Header("Fade Timer and Bool: ")]
    //public float fadeTime = 3f;
    public bool isFadeing;
    //[HideInInspector] public Material m;
    //[HideInInspector] public Color c;
    //[HideInInspector] public Color t;

    //GHOSTING FX
    [Header("Grey Ghost FX:")]
    public ParticleSystem psBody;
    public ParticleSystem psTop;
    public ParticleSystem psBottom;
    public ParticleSystem psVapor;

    //RING FX
    [Header("Ring Particles: ")]
    public ParticleSystem psRing;

    //RANGEFINDING
    [Header("Range Feedback: ")]
    public float range;

    //ALIVE BOOL, PUBLIC FOR DEBUG:
    [Header("Awareness Feedback: ")]
    public bool greyAlive;
    public bool isSeeing;
    public bool isHearing;
    public bool isAlert;
    public bool isAlarmed;

    //DEBUG PLAYER INPUT
    [Header("Movement Feedback: ")]
    public bool isMoveing;
    public bool isIdle;
    public bool isWalkingForward;
    public bool isWalkingBackward;
    public bool isTurningRight;
    public bool isTurningLeft;

    //UI FEEDBACK
    [Header("UI Feedback: ")]
    public Text isAlive;
    public Text isState;
    public Text inRange;

    //HIDDEN PUBLIC COMPONENTS AND VARIABLES: 
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator anim;
    [HideInInspector] public float stateTimeElapsed;

    //BEFORE WE START, GET THE COMPONENTS WE NEED:
    private void Awake() {

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

    }

    //ON START, VERIFY COMPONENTS AND SET BOOL "greyAlive":
    public void Start() {

        if (anim) {

            greyAlive = true;
            isAlive.enabled = true;
            Debug.Log("Grey is alive");

        } else {

            greyAlive = false;
            isAlive.enabled = false;
            Debug.Log("Something is wrong with Components");

        }

        //StartCoroutine(SetFade());

    }

    //UPDATE TO MAINTAIN OR CHANGE CURRENT STATE:
    private void FixedUpdate() {

        //QUIT IF WE DON'T HAVE WHAT WE NEED:
        if (!greyAlive) {

            return;

        }

        //OTHERWISE UPDATE THE STATE STATUS:
        currentState.UpdateState(this);

        //FEEDBACK UI
        isAlive.text = "I'm Alive";
        isState.text = currentState.ToString();
        inRange.text = range.ToString();
        Debug.Log("Current State:  " + currentState.ToString());

        //RANGEFINDING
        Vector3 player2d = new Vector3(player.transform.position.x, 0f, player.transform.position.z);
        Vector3 winter2d = new Vector3(this.transform.position.z, 0f, this.transform.position.z);
        range = Vector3.Distance(player2d, winter2d);
        //Debug.Log("Range:  " + range);

        //SET MOVEING BOOLS
        isMoveing = isWalkingForward || isWalkingBackward || isTurningLeft || isTurningRight;
        isIdle = !isMoveing;

        //MECANIM SETTERS
        //IDLE
        anim.SetBool("idle", isIdle);
        //MOVEING
        anim.SetBool("moveing", isMoveing);
        anim.SetBool("turnRight", isTurningRight);
        anim.SetBool("turnLeft", isTurningLeft);
        anim.SetBool("walkForward", isWalkingForward);
        anim.SetBool("walkBackward", isWalkingBackward);
        //FEEDBACK STATES
        anim.SetBool("alert", isAlert);

        //PLAYER INPUT DEBUG 
        //RIGHT TURN
        isTurningRight = Input.GetKey(KeyCode.RightArrow);
        //LEFT TURN
        isTurningLeft = Input.GetKey(KeyCode.LeftArrow);
        //WALK FORWARD
        isWalkingForward = Input.GetKey(KeyCode.UpArrow);
        //WALK BACKWARD
        isWalkingBackward = Input.GetKey(KeyCode.DownArrow);
      
        //LERP FADER
        if (Input.GetKeyDown(KeyCode.Space)) {

            //StartCoroutine(Timer());

        }
      

    }


    //PERSIONAL SPACE
    private void OnTriggerStay(Collider other) {

        isAlarmed = true;

    }

    private void OnTriggerExit(Collider other) {

        isAlarmed = false;

    }

   

    //TIMER
    public IEnumerator Timer(){

        float elapsedTime = 0f;

        while (elapsedTime <= startTime) {

            timerStarted = true;
            timerEnded = false;
            elapsedTime += Time.deltaTime;
            countdown = elapsedTime;


            if (elapsedTime >= startTime){

                timerStarted = false;
                timerEnded = true;
                yield break;

            }

            Debug.Log("Timer: " + elapsedTime);
            yield return null;

        }


    }


    //DRAW GIZMOS FOR FEEDBACK AND DEBUG:
    private void OnDrawGizmos() {
        
        if(currentState != null && greyEyes != null){

                Gizmos.color = currentState.sceneGizmoColor;
                Gizmos.DrawWireSphere(greyEyes.position, status.lookSphereCastRadius);

        }

    }

    //TRANSITION BETWEEN STATES:
    public void TransitionToState(State nextState){

        if(nextState != remainState){

                currentState = nextState;
                OnExitState();

        }

    }

    //CHECK STATE COUNTDOWN TIMER:
    public bool CheckIfCountDownElapsed(float duration){

        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);

    }

    //ON EXIT STATE RESET TIME ELAPSED TO 0:
    private void OnExitState(){

        stateTimeElapsed = 0f;

    }



}
