using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalSpace : MonoBehaviour {

    protected Animator anim;
    contactlocater contact;
    
    //BOOL
    public bool ikActive = false;     
    //RIGHT HAND
    Transform righthandobj = null;
    Transform righthandDown = null;
    //LEFT HAND
    Transform lefthandobj = null;
    Transform lefthandDown = null;
    //LOOK AT THIS
    Transform lookatobj = null;
    //IK CONTROLS
    float state = 0;
    float elapsedTime = 0;
    public float timeReact = 0.5f;
    public float Timer = 10.0f;
    //HAND SWITCHING FROM CONTACT ANGLE


    //PUBLIC FLOATS FOR WEIGHTS
    public float w_main;
    public float w_body;
    public float w_head;
    public float w_eyes;
    public float w_clamp;



    void Start() {

        anim = GetComponent<Animator>();
        contact = GameObject.FindGameObjectWithTag("personalspace").GetComponent<contactlocater>();
        righthandobj = GameObject.FindGameObjectWithTag("ikrighthand").GetComponent<Transform>();
        righthandDown = GameObject.FindGameObjectWithTag("righthand").GetComponent<Transform>();
        lookatobj = GameObject.FindGameObjectWithTag("iklookat").GetComponent<Transform>();

        Vector3 lookAtTargetPosition = lookatobj.position;

    }



    private void OnAnimatorIK() {

        Timer = Timer -= Time.deltaTime * 0.3f;
        if (Timer <= 0.0f) {

            Timer = 1.0f;
            Debug.Log(Timer);

        }

        if (anim & contact.right) {

            //REACH RIGHT
            if (ikActive) {

                //SET HAND FLOAT
                anim.SetFloat("rhand", 1, timeReact, Time.deltaTime * 0.3f);
                //USE HAND FOR POS WEIGHT
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, anim.GetFloat("rhand"));
                //anim.SetIKRotationWeight(AvatarIKGoal.RightHand, anim.GetFloat("rhand"));
                //SET HAND POSITION
                anim.SetIKPosition(AvatarIKGoal.RightHand, righthandobj.position);
                //anim.SetIKRotation(AvatarIKGoal.RightHand, righthandobj.rotation);                   
                //LOOK AT     
                //float lookAtWeight = Mathf.Lerp(0, lookatobj.position.magnitude, Time.deltaTime * timeReact);
                anim.SetLookAtWeight(anim.GetFloat("rhand"), w_body, w_head, w_eyes, w_clamp);
                anim.SetLookAtPosition(lookatobj.position);


            }  else {

                //SET HAND FLOAT               
                anim.SetFloat("rhand", 0, timeReact, Time.deltaTime * 1f);
                //USE HAND FOR POS WEIGHT
                Timer = 1.0f;
                anim.SetFloat("timer", Timer);
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, anim.GetFloat("rhand"));
                anim.SetIKRotationWeight(AvatarIKGoal.RightHand, anim.GetFloat("rhand"));
                //SET HAND POSITION
                anim.SetIKPosition(AvatarIKGoal.RightHand, righthandobj.position);
                anim.SetIKRotation(AvatarIKGoal.RightHand, righthandobj.rotation);              
                //LOOK AT     
                //float lookAtWeight = Mathf.Lerp(0, lookatobj.position.magnitude, Time.deltaTime * timeReact);
                anim.SetLookAtWeight(anim.GetFloat("rhand"), w_body, w_head, w_eyes, w_clamp);
                anim.SetLookAtPosition(lookatobj.position);

            } 

        }

        if (anim & contact.left) {

            //REACH LEFT
            if (ikActive) {

                //SET HAND FLOAT
                anim.SetFloat("lhand", 1, timeReact, Time.deltaTime * 0.3f);
                //USE HAND FOR POS WEIGHT
                anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, anim.GetFloat("lhand"));
                anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, anim.GetFloat("lhand"));
                //SET HAND POSITION
                anim.SetIKPosition(AvatarIKGoal.LeftHand, righthandobj.position);
                anim.SetIKRotation(AvatarIKGoal.LeftHand, righthandobj.rotation);
                //LOOK AT     
                //float lookAtWeight = Mathf.Lerp(0, lookatobj.position.magnitude, Time.deltaTime * timeReact);
                anim.SetLookAtWeight(anim.GetFloat("lhand"), w_body, w_head, w_eyes, w_clamp);
                anim.SetLookAtPosition(lookatobj.position);


            } else {

                //SET HAND FLOAT               
                anim.SetFloat("lhand", 0, timeReact, Time.deltaTime * 1f);
                //USE HAND FOR POS WEIGHT
                Timer = 1.0f;
                anim.SetFloat("timer", Timer);
                anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, anim.GetFloat("lhand"));
                anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, anim.GetFloat("lhand"));
                //SET HAND POSITION
                anim.SetIKPosition(AvatarIKGoal.LeftHand, righthandobj.position);
                anim.SetIKRotation(AvatarIKGoal.LeftHand, righthandobj.rotation);
                //LOOK AT     
                //float lookAtWeight = Mathf.Lerp(0, lookatobj.position.magnitude, Time.deltaTime * timeReact);
                anim.SetLookAtWeight(anim.GetFloat("lhand"), w_body, w_head, w_eyes, w_clamp);
                anim.SetLookAtPosition(lookatobj.position);

            }

        }
    }
}
