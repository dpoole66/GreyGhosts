using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour{

    //GREY CONTROLLER
    GreyAwareness awareness;

    //RANGEFINDING    
    Transform PlayerEyes;
    Transform GreyEyes;
    [Header("Distance to Grey: ")]
    public float range;

    //VISION
    [Header("Vision/Awareness: ")]
    public bool isLooking;
    public bool  iSeeSomething;
    [Header("Vision Range: ")]
    public float visionRange = 10f;
    [Header("Final Vision Scale: ")]
    [Range(1f, 2f)]
    public float sphereScaleFactor;
    public float sphereScale = 1f;
    [HideInInspector]
    public Vector3 hitPoint;
    [HideInInspector]
    public Transform playerEyesCur;

    //[Header("Feedback: ")]
    //public Text feedback;

    void Start()
    {

        PlayerEyes = GameObject.FindGameObjectWithTag("playerEye").GetComponent<Transform>();       
        GreyEyes = GameObject.FindGameObjectWithTag("greyEye").GetComponent<Transform>();
        awareness = GameObject.FindGameObjectWithTag("grey").GetComponent<GreyAwareness>();
        isLooking = true;

    }

    private void FixedUpdate() {

        if(!GreyEyes || !awareness){

                return;

        }

        range = Vector3.Distance(PlayerEyes.transform.position, GreyEyes.transform.position);
        sphereScale = range / sphereScaleFactor;

        if(isLooking){

            RaycastHit hit;

            if (Physics.SphereCast(PlayerEyes.transform.position, sphereScale, PlayerEyes.forward, out hit, visionRange)) {

                hitPoint = hit.point;
                Debug.DrawRay(PlayerEyes.transform.position, PlayerEyes.forward, Color.magenta);

                if (hit.collider.tag == "grey") {

                    Debug.Log("I see " + hit.collider.tag);
                    //feedback.text = "I see " + hit.collider.tag;
                    iSeeSomething = true;
                    //TOGGLE GREY BOOL
                    awareness.isBeingLookedAt = true;

                }

            } else {

                Debug.Log("I don't see anything.");
                //feedback.text = "I don't see anything";
                iSeeSomething = false;
                //TOGGLE GREY BOOL
                awareness.isBeingLookedAt = false;

            }

        }

        

    }


   

    void OnDrawGizmos() {
        //VISION SPHERE SCALE DEBUG

        if (isLooking) {

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(GreyEyes.transform.position, sphereScale);

            //Gizmos.DrawLine(PlayerEyes.forward, hitPoint);

        }

    }
}
