using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class DoorController : MonoBehaviour{

    [Header("Door Elements: ")]
    public Transform hinge;
    [Header("Door State and Settings: ")]
    public bool inTrigger;   
    public float openSpeed = 2f;
    public bool isOpen = false;
    private float openAngle;
    private Quaternion open = Quaternion.Euler(0f, 160f, 0f);
    private Quaternion closed = Quaternion.Euler(0f, 0f, 0f);            
    private NavMeshAgent audrey;


    void Start() {

        hinge = GameObject.FindGameObjectWithTag("doorHinge").GetComponent<Transform>();
        audrey = GameObject.FindGameObjectWithTag("audrey").GetComponent<NavMeshAgent>();

    }

   
    void Update(){

        Debug.Log("open: " + open + " closed: " + closed + "  Nav: "  + audrey.isStopped);
        audrey.isStopped = inTrigger && !isOpen;

        if (inTrigger && !isOpen){

            Debug.Log("This is the Door Controller and I am OPENING");              
            openAngle = Quaternion.Angle(closed, open);
            StartCoroutine(DoorOpen(hinge.transform.rotation, open, openAngle/openSpeed));

        }

        if (!inTrigger && isOpen) {

            Debug.Log("This is the Door Controller and I am CLOSING");
            openAngle = Quaternion.Angle(open, closed);
            StartCoroutine(DoorClose(hinge.transform.rotation, closed, openAngle / openSpeed));

        }

        if(!inTrigger && !isOpen || inTrigger && isOpen){

            Debug.Log("Stopping All Coroutines");
            StopAllCoroutines();

        }

    }

    public IEnumerator DoorOpen(Quaternion start, Quaternion end, float time) {

        if (isOpen) {

            yield break;

        }

        float t = 0f;
        while (t < time) {

            hinge.transform.rotation = Quaternion.Slerp(start, end, t / time);
            yield return null;
            t += Time.deltaTime;

        }

        hinge.transform.rotation = end;
        isOpen = true;

    }

    public IEnumerator DoorClose(Quaternion start, Quaternion end, float time) {

        if (!isOpen) {

            yield break;

        }

        float t = 0f;
        while (t < time) {

            hinge.transform.rotation = Quaternion.Slerp(start, end, t / time);
            yield return null;
            t += Time.deltaTime;

        }

        hinge.transform.rotation = end;
        isOpen = false;

    }


}
