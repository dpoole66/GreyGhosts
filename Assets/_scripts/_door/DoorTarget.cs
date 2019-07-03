using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTarget : MonoBehaviour{

    public Transform moveToPos;
    private GameObject goTellAudrey;   

    void Start(){

       
        goTellAudrey = GameObject.FindGameObjectWithTag("audrey");

    }

    private void OnTriggerEnter(Collider other) {

        moveToPos = this.transform.Find("insidePos").gameObject.transform;
        goTellAudrey.SendMessage("DoorTarget", moveToPos);
        Debug.Log("Collider sending Message to Audrey: " + moveToPos);

    }
}
