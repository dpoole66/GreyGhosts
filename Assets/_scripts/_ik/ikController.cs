using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ikController : MonoBehaviour
{
    protected Animator anim;
    public bool ikActive = false;

    public Transform rightHandObj;
    public Transform leftHandObject;
    public Transform lookAtObject;
    public bool lookAt= false;
    public float lookAtWeight = 0.0f;

    //LERP
    float state = 0f;
    float elapsedTime = 0f;
    [Header("IK Reaction Time: ")]
    public float timeReaction = 0.5f;

    private void Start() {

        anim = GetComponent<Animator>();
        state = 0f;

    }

    public void LookAtThis(Transform lookHere){

        lookAtObject.transform.position = lookHere.position;
        Debug.Log("Look Here Pos: " + lookHere.position);
        ikActive = true;
        lookAt = true;

    }

    private void OnAnimatorIK() {

         //////////////
        //LOOK AT
        if(ikActive && lookAt){

            if (lookAtObject != null) {

                if (state < 1.0f) {

                    elapsedTime += Time.deltaTime;
                    state = Mathf.Lerp(0, 1, elapsedTime / timeReaction);


                } else {

                    state = 1.0f;
                    elapsedTime = 0f;
          

                }


                if (state >= 1f) {

                    elapsedTime += Time.deltaTime;
                    state = Mathf.Lerp(0, 1, elapsedTime / timeReaction);

                }
            } 

        }

        //LOOK AT
        anim.SetLookAtWeight(state, 0.1f, 0.5f, 0.7f, 0.5f);
        anim.SetLookAtPosition(lookAtObject.position);

        //SET HAND WEIGHTS
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, state);
        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, state);
        //SET POS
        anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
        anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObject.position);
        //SET ROT
        anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
        anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObject.rotation);

        lookAtWeight = state;
    }


    //LOOK AT
    public void LookAtThisPoint(Transform target) {

        lookAtObject.position = target.position;
        ikActive = true;
        lookAt = true;

    }

    public void OpenDoorInsideIK(){

        lookAtObject = rightHandObj;
        ikActive = !ikActive;                       

    }

    public void ReleaseDoorInsideIK() {

        ikActive = false;            

    }
}
