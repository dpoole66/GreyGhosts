using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour{

    public Camera[] spyCams;

    void Start(){

        //DISABLE ALL SPY CAMS AT START

        foreach(Camera spyCam in spyCams){

            spyCam.enabled = false;

        }
        
    }
   
}
