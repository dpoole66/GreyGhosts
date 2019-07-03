using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Winter : MonoBehaviour
{
    //STATE AND FEEDBACK: 
    [Header("Current State Update: ")]
    public State currentState;
    public State remainState;

    //REQUIRED COMPONENTS:
    [Header("Required Components: ")]
    public GreyStatus status;
    public Transform greyEyes;

    //ALIVE BOOL, PUBLIC FOR DEBUG:
    public bool greyAlive;

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
        
        if(anim && agent){

            greyAlive = true;
            Debug.Log("Grey is alive");

        }  else{

            greyAlive = false;
            Debug.Log("Something is wrong with Components");

        }

    }

    //UPDATE TO MAINTAIN OR CHANGE CURRENT STATE:
    private void Update() {
       
        //QUIT IF WE DON'T HAVE WHAT WE NEED:
        if(!greyAlive){

                return;

        }

        //OTHERWISE UPDATE THE STATE STATUS:
        currentState.UpdateState(this);

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
