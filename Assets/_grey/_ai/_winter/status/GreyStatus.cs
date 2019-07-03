using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WinterAI/Data/Status")]

public class GreyStatus : ScriptableObject
{
    [Header("Vision: ")]
    public float visionRange;
    public bool isSeeing;
    [Header("Width of Vision Cast: ")]
    public float lookSphereCastRadius;

    [Header("Hearing: ")]
    public float hearingRange;
    public bool isHearing;
    
    [Header("Touch: ")]
    public float touchRange;
    public bool isTouching;

    [Header("VISION DEBUG")]
    public float visionLevel;
    public float visionDamping;
    [Header("HEARING DEBUG")]
    public float hearingLevel;
    public float hearingDamping;
    [Header("TOUCH DEBUG")]
    public float touchDamping;


}
