﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightController : MonoBehaviour {

    public GameObject SpotlightTarget;

    private Transform ThisTransform = null;
    public float RotSpeed = 90f;

     private Transform Target = null;

    public float Damping = 55f;
    //---------------------------------------------------
    // Use this for initialization
    void Awake() {
        ThisTransform = GetComponent<Transform>();
        //Target = GameObject.FindGameObjectWithTag("Audrey").transform;
    }
    //---------------------------------------------------
    // Update is called once per frame
    void Update() {

          if(SpotlightTarget){

            Target = GameObject.FindGameObjectWithTag("Audrey").transform;
            //RotateTowards();
            RotateTowardsWithDamp();

          }
       
    }
    //---------------------------------------------------
    void RotateTowards() {
        //Get look to rotation
        Quaternion DestRot = Quaternion.LookRotation(Target.position - transform.position, Vector3.up);

        //Update rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, DestRot, RotSpeed * Time.deltaTime);
    }
    //---------------------------------------------------
    void RotateTowardsWithDamp() {
        //Get look to rotation
        Quaternion DestRot = Quaternion.LookRotation(Target.position - transform.position, Vector3.up);

        //Calc smooth rotate
        Quaternion smoothRot = Quaternion.Slerp(transform.rotation, DestRot, 1f - (Time.deltaTime * Damping));

        //Update Rotation
        transform.rotation = smoothRot;
    }
}
