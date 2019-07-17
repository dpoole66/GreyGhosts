using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore.Examples.HelloAR;

public class ARRootScaler : MonoBehaviour {

    Transform ARRoot;
    public ARController ARController;

 
	void Start () {

        //ARController = GetComponent<DebugScaler>();

	}
	

	void Update () {

        this.transform.localScale = new Vector3(ARController.ARScale, ARController.ARScale, ARController.ARScale);

    }
}
