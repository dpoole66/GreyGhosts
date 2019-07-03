using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreyAnxiety : MonoBehaviour
{

    //REQUIRED GREY    
    public GameObject grey;   
    GreyAwareness aware;

    [Header("ANXIETY AND RATE")]
    [Range(-0f, 100f)]
    public float anxiety = 0f;
    public float anxietyRate = 10f;


    private void Start() {

        aware = grey.GetComponent<GreyAwareness>();

    }

}
