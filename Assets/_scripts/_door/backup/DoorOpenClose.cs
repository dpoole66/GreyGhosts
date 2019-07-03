using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorOpenClose : MonoBehaviour {

    [Header("Door Elements: ")]
    public Transform hinge;

    [Header("Door State and Settings: ")]
    public bool inTrigger;
    //public float openSpeed = 2f;
    public bool isOpen = false;
    private Quaternion open = Quaternion.Euler(0f, 160f, 0f);
    private Quaternion closed = Quaternion.Euler(0f, 0f, 0f);
    private NavMeshAgent audrey;

    [Header("NavMeshAgent Positioning for Interaction: ")]
    public GameObject goTellAudrey;
    public Transform outsideOpen;
    public Transform insideOpen;

    [Header("Door Control Values: ")]
    public DoorStats stats;

    private void Start() {

        stats.openAngle = Quaternion.Angle(closed, open);
        audrey = GameObject.FindGameObjectWithTag("audrey").GetComponent<NavMeshAgent>();

    }

    //private void OnTriggerEnter(Collider other) {

    //    if (other.tag == "audrey") {

    //        inTrigger = true;
    //        Debug.Log("ENTER");

    //        if(!isOpen){

    //            StartCoroutine(DoorOpen(hinge.transform.localRotation, open, stats.openAngle / stats.openSpeed));

    //        }

    //    }

    //}

    //private void OnTriggerStay(Collider other) {

    //    if (other.tag == "audrey") {

    //        inTrigger = true;

    //        if (!isOpen) {

    //            goTellAudrey.SendMessage("SetDoorInsideTarget", insideOpen);

    //        }
    //        Debug.Log("STAY");

    //    }

    //}

    //private void OnTriggerExit(Collider other) {

    //    if (other.tag == "audrey") {

    //        inTrigger = false;
    //        Debug.Log("EXIT");

    //        if(isOpen){

    //            StartCoroutine(DoorClose(hinge.transform.localRotation, closed, stats.openAngle / stats.openSpeed));

    //        }

    //    }

    //}

    public IEnumerator DoorOpen(Quaternion start, Quaternion end, float time) {

        if (isOpen) {

            yield break;

        }

        goTellAudrey.SendMessage("SetDoorInsideTarget");

        yield return new WaitForSeconds(1);
        goTellAudrey.SendMessage("OpenDoorInsideIK");

        yield return new WaitForSeconds(0.7f);
        goTellAudrey.SendMessage("ReleaseDoorInsideIK");

        float t = 0f;
        while (t < time) {

            audrey.isStopped = true;
            hinge.transform.localRotation = Quaternion.Slerp(start, end, t / time);
            yield return null;
            t += Time.deltaTime;

        }

        hinge.transform.localRotation = end;
        audrey.isStopped = false;
        isOpen = true;

    }

    public IEnumerator DoorClose(Quaternion start, Quaternion end, float time) {

        if (!isOpen) {

            yield break;

        }

        float t = 0f;
        while (t < time) {

            hinge.transform.localRotation = Quaternion.Slerp(start, end, t / time);
            yield return null;
            t += Time.deltaTime;

        }

        hinge.transform.localRotation = end;
        isOpen = false;

    }


    public void DoorInsideOpen(){

        StartCoroutine(DoorOpen(hinge.transform.localRotation, open, stats.openAngle / stats.openSpeed));

    }

    public void DoorInsideClose() {

        StartCoroutine(DoorClose(hinge.transform.localRotation, closed, stats.openAngle / stats.openSpeed));

    }

}
