using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour{

    public Camera cam;

    private void Start() {

        //cam = GameObject.FindObject  GetComponent<Camera>();

    }

    private void OnTriggerEnter(Collider other) {

        Debug.Log("Camera:  " + cam.name);
        cam.enabled = true;

    }

    private void OnTriggerExit(Collider other) {

        Debug.Log("Camera:  " + cam.name);
        cam.enabled = false;

    }
}
