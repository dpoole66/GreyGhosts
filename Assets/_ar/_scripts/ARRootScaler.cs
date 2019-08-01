using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore.Examples.HelloAR;
using GoogleARCore;

public class ARRootScaler : MonoBehaviour {

    public WinterAR ARController;

 
	void Start () {

        // = GameObject.FindGameObjectsWithTag("arroot").GetComponent<WinterAR>();

	}
	

	void Update () {

        this.transform.localScale = new Vector3(ARController.arScaleFactor, ARController.arScaleFactor, ARController.arScaleFactor);

    }
}
