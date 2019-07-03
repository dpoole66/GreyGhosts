using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurnAway : MonoBehaviour {

    public int violations;
    NavMeshAgent agent;
    Animator anim;
    private Transform turnTarget, faceTarget;
    //private PersonalSpace personal;


	void Start () {

        violations = 0;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        //personal = GetComponent<PersonalSpace>();
        turnTarget = GameObject.FindGameObjectWithTag("turnTarget").GetComponent<Transform>();
        faceTarget = GameObject.FindGameObjectWithTag("faceTarget").GetComponent<Transform>();

    }


	void Update ()     { 

        Debug.Log(violations);

        if(violations == 2){

            //personal.enabled = false; ;
            //Vector3 destination = turnTarget.transform.position;
            //agent.destination = destination;
            anim.SetTrigger("action3");

        }

        if (violations == 4) {

            //Vector3 destination = faceTarget.transform.position;
            //agent.destination = destination;
            //personal.enabled = true; 
            anim.SetTrigger("action3");

        }

        if(violations > 5){

            violations = 0;
            //personal.enabled = true;

        }



    }
}
