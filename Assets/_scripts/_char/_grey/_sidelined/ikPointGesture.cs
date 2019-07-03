using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RealisticEyeMovements;

public class ikPointGesture : MonoBehaviour {

    protected Animator anim;
    public bool ikActive = false;
    public float timeReact = 4f;
    //FINGERS
    public Transform index;
    public Transform mid;
    public Transform ring;
    public Transform little;
    public float fingerRotation;
    Vector3 fingerRot;
    Vector3 fingerRelax;
    bool isPointing = false;
    //HAND
    Transform rightHand = null;
    Transform pointTarget = null;
    float handWeight = 0.0f;
    Vector3 pointAtTargetPosition;
    Vector3 pointAtPosition;

    public float heat = 1.0f;
    public float cool = 3.0f;

    public int looked = 0;
    //public bool looking = false;

    EyeAndHeadAnimator eyeHead;
    LookTargetController lookTarget;


    void Start() {

        anim = GetComponent<Animator>();
        pointTarget = GameObject.FindGameObjectWithTag("pointTarget").GetComponent<Transform>();
        rightHand = GameObject.FindGameObjectWithTag("righthand").GetComponent<Transform>();
        index = anim.GetBoneTransform(HumanBodyBones.RightIndexProximal);
        mid = anim.GetBoneTransform(HumanBodyBones.RightMiddleProximal);
        ring = anim.GetBoneTransform(HumanBodyBones.RightRingProximal);
        little = anim.GetBoneTransform(HumanBodyBones.RightLittleProximal);
        fingerRot = new Vector3(fingerRotation, 0, 0);
        fingerRelax = new Vector3(0, 0, 0);
        //SET HAND 
        pointAtTargetPosition = rightHand.position + transform.forward;

        lookTarget = GetComponent<LookTargetController>();
        eyeHead = GetComponent<EyeAndHeadAnimator>();

    }

    public void Looked(){

       looked ++;
       if (looked ==1){

            StartCoroutine(lookTime());

       }

    }

    public void LookedAway() {

        ikActive = false;

    }

    private void LateUpdate() {

        if (isPointing) {

            //handWeight = anim.GetFloat("rhand");
            mid.rotation = mid.rotation * Quaternion.Euler(fingerRot);
            ring.rotation = ring.rotation * Quaternion.Euler(fingerRot);
            little.rotation = little.rotation * Quaternion.Euler(fingerRot);
            //eyeHead.enabled = false;
            //lookTarget.enabled = false;

        } else {

            Quaternion fingersRelaxed = Quaternion.Euler(fingerRelax);

            mid.rotation = mid.rotation * Quaternion.Lerp(mid.rotation, fingersRelaxed, timeReact);
            ring.rotation = ring.rotation * Quaternion.Lerp(ring.rotation, fingersRelaxed, timeReact);
            little.rotation = little.rotation * Quaternion.Lerp(little.rotation, fingersRelaxed, timeReact);
            //eyeHead.enabled = true;
            //lookTarget.enabled = true;

        }
    }

    private void OnAnimatorIK() {

        isPointing = ikActive;
        float pointAtTargetWeight = ikActive ? 1.0f : 0.0f;

        Vector3 current = pointAtPosition - rightHand.position;
        Vector3 future = pointAtTargetPosition - rightHand.position;

        current = Vector3.RotateTowards(current, future, timeReact * Time.deltaTime, float.PositiveInfinity);
        pointAtPosition = rightHand.position + current;

        float blend = pointAtTargetWeight > handWeight ? heat : cool;
        handWeight = Mathf.MoveTowards(handWeight, pointAtTargetWeight, Time.deltaTime / blend);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, handWeight);
        anim.SetIKPosition(AvatarIKGoal.RightHand, pointTarget.position);
        anim.SetIKRotation(AvatarIKGoal.RightHand, pointTarget.rotation);

    }

    IEnumerator lookTime(){

        ikActive = true;
        yield return new WaitForSeconds(heat + cool);
        ikActive = false;

    }

}
