using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreyController : MonoBehaviour
{

    public Animator anim;
    Rigidbody rb;   
    public float moveSpeed = 33f;
    public float rotateSpeed = 33f;
    public float range;
    //LOOK STATES
    [Header("Look States: ")]
    public bool isLooking = false;
    //ACTIVE STATES
    [Header("Active States: ")]
    public bool isIdle = false;
    public bool isAware = false;

    private void Start() {

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        CurrentState = GREYSTATE.IDLE;

    }


    //State machine for Actions
    public enum GREYSTATE { IDLE, AWARE, APPROACH, WITHDRAW };
    [SerializeField] GREYSTATE currentState = GREYSTATE.IDLE;

    public GREYSTATE CurrentState{

        get{ return currentState; }
        set{

                currentState = value;
                StopAllCoroutines();

                switch(currentState){

                    case GREYSTATE.IDLE:
                        StartCoroutine(GreyIdle());
                        break;

                    case GREYSTATE.AWARE:
                        StartCoroutine(GreyAware());
                        break;

                    case GREYSTATE.APPROACH:
                        StartCoroutine(GreyApproach());
                        break;

                    case GREYSTATE.WITHDRAW:
                        StartCoroutine(GreyWithdraw());
                        break;

                }

        }

     }

     public IEnumerator GreyIdle(){

         while (true){

            isIdle = true;
            isAware = false;
            anim.SetBool("idle", true);
            anim.SetBool("alert", false);

            if (isAware) {

                CurrentState = GREYSTATE.AWARE;
                yield break;

            }

            yield return null;

          }

     }

    public IEnumerator GreyAware() {

        isIdle = false;
        isAware = true;
        anim.SetBool("idle", false);
        anim.SetBool("alert", true);

        if (!isLooking) {

            CurrentState = GREYSTATE.IDLE;
            yield break;

        }

        yield return null;

    }

    public IEnumerator GreyApproach() {

        yield return null;

    }

    public IEnumerator GreyWithdraw() {

        yield return null;

    }

   

}
