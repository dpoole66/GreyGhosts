using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contactlocater : MonoBehaviour {

    //BOOLS
    [Header ("RIGHT & LEFT")]
    public bool right;
    public bool left;
    public bool center;

    void OnTriggerStay(Collider other) {

        RaycastHit hit;

        if(Physics.SphereCast(transform.position, 0.25f, transform.forward, out hit))     {

            Vector3 targetDir = other.transform.up - hit.point;
            Vector3 angle = Vector3.Cross(hit.point, targetDir);
            float dir = Vector3.Dot(angle, other.transform.up);

            Debug.DrawLine(hit.point, other.transform.position, Color.red);

            if(dir <= -5.0f){

                Debug.Log("RIGHT: " + angle);
                right = true;
                left = false;

            }else if (dir >= 5.0f) {

                Debug.Log("LEFT: " + angle);
                right = false;
                left = true;

            }else{

                Debug.Log("CENTER: " + angle);
                center = dir >= -5.0f && dir <= 5.0f;

            }

        }

    }
}
