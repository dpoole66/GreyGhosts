using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreyMotion : MonoBehaviour
{

    private Transform ThisTransform = null;
    public float RotSpeed = 90f;              
    public float Damping = 55f;
    public Transform Target;

    void Awake() {

        ThisTransform = GetComponent<Transform>();
        Target = GetComponent<Transform>();
    }

    void Update() {

        ThisTransform.LookAt(Target.transform);   

    }
    

    void RotateTowardsWithDamp() {

        Quaternion DestRot = Quaternion.LookRotation(Target.position - ThisTransform.position, Vector3.up);          
        Quaternion smoothRot = Quaternion.Slerp(transform.rotation, DestRot, 1f - (Time.deltaTime * Damping));    
        transform.rotation = smoothRot;

    }
}
