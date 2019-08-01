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
    public Rigidbody rb;
    public NavMeshAgent agent;

    [Header("Timer: ")]
    public float countdown;
    public float startTime;
    public bool timerStarted;
    public bool timerEnded;

    [Header("REM: ")]
    public RealisticEyeMovements.LookTargetController lookTargeting;

    [Header("Get Player: ")]
    public Transform player;
    Vector3 relativePlayerPos;

    [Header("Get Targets: ")]
    public GameObject target;
    Vector3 relativeTargetPos;

    //ROTATION 
    Quaternion deltaRotation;

    [Header("Lerper: ")]
    public Lerper lerper;

    [Header("Fade In/Out bool: ")]
    public bool isFadeing;

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
    public float targetRange;

    //ALIVE BOOL, PUBLIC FOR DEBUG:
    [Header("Awareness Feedback: ")]
    public bool greyAlive;
    public bool isSeeing;
    public bool isHearing;
    public bool isAlert;
    public bool isAlarmed;

    //DEBUG PLAYER INPUT
    [Header("Movement Feedback: ")]
    public bool goTo;
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
    [HideInInspector] public Animator anim;
    [HideInInspector] public float stateTimeElapsed;

    //BEFORE WE START, GET THE COMPONENTS WE NEED:
    private void Awake() {

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
        Vector3 target2d = new Vector3(target.transform.position.x, 0f, target.transform.position.z);
        Vector3 player2d = new Vector3(player.transform.position.x, 0f, player.transform.position.z);
        Vector3 winter2d = new Vector3(this.transform.position.z, 0f, this.transform.position.z);
        range = Vector3.Distance(player2d, winter2d);
        targetRange = Vector3.Distance(target2d, winter2d);
        //Debug.Log("Target Range:  " + targetRange);

        //MECANIM SETTERS
        //IDLE
        anim.SetBool("idle", isIdle);
        //MOVEING
        anim.SetBool("moveing", isMoveing);

        //FEEDBACK STATES
        anim.SetBool("alert", isAlert);

        //MOVE AND ROTATE TOWARD PLAYER/TARGET 
        relativePlayerPos = player2d - winter2d;
        relativeTargetPos = target2d - winter2d;

        //MOVE DEBUG
        isIdle = !isMoveing;

        if(Input.GetKey(KeyCode.Space)){

            goTo = true;

        }
        if (Input.GetKeyUp(KeyCode.Space)) {

            goTo = false;

        }


        ////// Pull character towards agent
        Vector3 worldDeltaPosition = agent.nextPosition - this.transform.position;
        //if (worldDeltaPosition.magnitude > agent.radius)
        //transform.position = agent.nextPosition - 0.9f * worldDeltaPosition;

        // Pull agent towards character
        if (worldDeltaPosition.magnitude > agent.radius)
            agent.nextPosition = transform.position + 0.9f * worldDeltaPosition;


    }

    void OnAnimatorMove() {
        // Update position based on animation movement using navigation surface height
        Debug.Log("AnimatorMove");
        Vector3 position = anim.rootPosition;
        position.y = agent.nextPosition.y;
        this.transform.position = position;
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
