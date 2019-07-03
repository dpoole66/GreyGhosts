using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoorData", menuName = "Doors", order = 51)]

public class DoorStats : ScriptableObject{

    [Header("Door Speed: ")]
    public float openSpeed = 33f;                                                        
    public float openAngle = 165f;

    [Header("Door Target Bools: ")]
    public Transform inSidePos;
    public Transform outSidePos;

    [Header("Door Target Bools: ")]
    public bool inSide;
    public bool outSide;

    private bool  InSide{ get; set; }
    private bool OutSide { get; set; }


}
