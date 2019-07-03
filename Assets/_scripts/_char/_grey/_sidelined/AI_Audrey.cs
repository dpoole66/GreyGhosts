using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Audrey : MonoBehaviour {

    //COMPONENTS
    private Animator anim = null;
    private NavMeshAgent nav = null;
    public Transform[] waypoints;
    private int currentPos = 0;
    //LOOKING AND  # OF TIMES LOOKING
    public int looked = 0;
    public bool looking = false;
    //TIMER
    public float currentTimerValue;
    //PATROL
    public float patrolDistance = 0.5f;
    float distance;


    //METHODS ETC
    private void Awake() {

        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

    }

    private void Start() {

        CurrentState = AUD_STATE.PATROL;

    }

    //FSM
    public enum AUD_STATE { PATROL, AWARE, WAVE }

    [SerializeField]
    private AUD_STATE currentState = AUD_STATE.PATROL;

    public AUD_STATE CurrentState {

        //GET AND SET ACCESSOR METHOD
        get { return currentState; }
        set {

            currentState = value;
            StopAllCoroutines();

            //SWITCHES
            switch (currentState) {

                case AUD_STATE.PATROL:
                    StartCoroutine(AIPatrol());
                    break;

                case AUD_STATE.AWARE:
                    StartCoroutine(AIAware());
                    break;

                case AUD_STATE.WAVE:
                    StartCoroutine(AIWave());
                    break;

            }
        }
    }

    //STATE BEHAVIOUR  
    public IEnumerator AIPatrol() {

        while (currentState == AUD_STATE.PATROL) {

            Debug.Log("Patrol");

            distance = Vector3.Distance(this.transform.position, waypoints[currentPos].position);

            if (distance <= patrolDistance) {

                currentPos++;

                if (currentPos == waypoints.Length) {

                    currentPos = 0;

                }

                nav.SetDestination(waypoints[currentPos].position);

            }

            yield return null;

        }

    }

    public IEnumerator AIAware() {

        while (currentState == AUD_STATE.AWARE) {
            Debug.Log("Aware");
            nav.isStopped = true;
            StartCoroutine(Timer());

            if(currentTimerValue == 0f){

                StartCoroutine(AIWave());

                yield break;

            }
            yield return null;
         }

    }

    public IEnumerator AIWave() {

        while (currentState == AUD_STATE.WAVE) {

            anim.SetBool("action1", true);
            yield return new WaitForSeconds(2f);
            anim.SetBool("action1", false);
            CurrentState = AUD_STATE.AWARE;
            yield break;

         }
 
    }


    //COUNTDOWN TIMER
    public IEnumerator Timer(float value = 10f){

        currentTimerValue = value;

         while (currentTimerValue > 0f) {

            yield return new WaitForSeconds(4f);
            currentTimerValue--;

            Debug.Log(currentTimerValue);
  
          }
        
    }


    //IF LOOKING OR NOT
    public void Looked() {

        looked++;
        looking = true;

    }

    public void LookedAway() {

        looking = false;

    }

   

    private void Update() {

        //nav.SetDestination(patrolDest.position);

    }



}



