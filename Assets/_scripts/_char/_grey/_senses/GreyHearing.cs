using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreyHearing : MonoBehaviour 
{

    public GameObject grey;
    public GreySpeech speech;
    GreyAwareness aware; 

    private void Start() {

        aware = grey.GetComponent<GreyAwareness>();

    }


  //START HEARING METHOD
  public void HearingOn(){

        speech.Attention();

  }
}
