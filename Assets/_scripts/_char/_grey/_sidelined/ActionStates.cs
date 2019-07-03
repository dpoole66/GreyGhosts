using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActionStates : MonoBehaviour {

    NavMeshAgent agent;
    Animator anim;
    AudPersonCharacter audpersoncontrol;

    //BOOLS FOR ACTION
    bool lookingAt = false;
    bool stoppedLookingAt = false;
    int lookCount = 0;

	// Use this for initialization
	void Start () {

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        audpersoncontrol = GetComponent<AudPersonCharacter>();
        anim.SetBool("idle", false);
		
	}
	
	// Update is called once per frame
	void Update () {

        if(agent.remainingDistance >= agent.stoppingDistance){

            anim.SetBool("idle", false);
            anim.SetBool("move", true);
            agent.isStopped = false;

        }   else{

            anim.SetBool("idle", true);
            anim.SetBool("move", false);
            agent.isStopped = true;

        }

    }

    public void LookAtTarget(){

        //Look
        LookAtAudrey lookAt = GetComponent<LookAtAudrey>();

        lookAt.lookAtTargetPosition = agent.steeringTarget + transform.forward;

        if (lookAt)
            lookAt.lookAtTargetPosition = agent.steeringTarget + transform.forward;

    }

    public void Act1(){

        lookingAt = true;

        if (lookCount <= 0) {

            StartCoroutine(A1());

            return;


        }

    }

    public void Act2() {

        stoppedLookingAt = true;
        StartCoroutine(A2());
        return;

    }

    public IEnumerator A1(){

        if(lookingAt){

            anim.SetBool("idle", false);
            anim.SetTrigger("action1");
            Debug.Log("ACTION 1");
            lookCount += 1;

            yield break;


        }


    }

    public IEnumerator A2() {

        if(stoppedLookingAt && lookCount > 0){

                anim.SetBool("idle", false);
                anim.SetTrigger("action2");
                Debug.Log("ACTION 2");
                lookCount -=1;
                yield break;

        }


    }

}
