using System.Collections;
using System.Collections.Generic;
using UnityEngine;              

public class DoorTrigger : MonoBehaviour{

    DoorController dc;

    private void Start() {

        dc = GameObject.FindGameObjectWithTag("doorControl").GetComponent<DoorController>();  

    }

    private void OnTriggerEnter(Collider other) {

        if (other.tag == "audrey") {

            dc.inTrigger = true;
            Debug.Log("ENTER");

        }

    }

    private void OnTriggerStay(Collider other) {

        if (other.tag == "audrey") {

            dc.inTrigger = true;          
            Debug.Log("STAY");

        }

    }

    private void OnTriggerExit(Collider other) {

        if (other.tag == "audrey") {

            dc.inTrigger = false;
            Debug.Log("EXIT");

        }

    }

    public IEnumerator DoorOpen(Quaternion start, Quaternion end, float time) {

        if (dc.isOpen) {

            yield break;

        }

        float t = 0f;
        while (t < time) {

            dc.hinge.transform.rotation = Quaternion.Slerp(start, end, t / time);
            yield return null;
            t += Time.deltaTime;

        }

        dc.hinge.transform.rotation = end;
        dc.isOpen = true;

    }

    public IEnumerator DoorClose(Quaternion start, Quaternion end, float time) {

        if (!dc.isOpen) {

            yield break;

        }

        float t = 0f;
        while (t < time) {

            dc.hinge.transform.rotation = Quaternion.Slerp(start, end, t / time);
            yield return null;
            t += Time.deltaTime;

        }

        dc.hinge.transform.rotation = end;
        dc.isOpen = false;

    }


}
