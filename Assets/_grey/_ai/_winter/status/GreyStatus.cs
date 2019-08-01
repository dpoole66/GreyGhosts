using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WinterAI/Data/Status")]

public class GreyStatus : ScriptableObject
{
    [Header("Vision: ")]
    public float visionRange;

    [Header("Width of Vision Cast: ")]
    public float lookSphereCastRadius;

    [Header("Hearing: ")]
    public float hearingRange;

    [Header("Touch: ")]
    public float touchRange;

    [Header("Critical Ranges: ")]
    public float alertRange;
    public float alarmRange;

    [Header("VISION DEBUG")]
    public float visionLevel;
    public float visionDamping;

    [Header("HEARING DEBUG")]
    public float hearingLevel;
    public float hearingDamping;

    [Header("TOUCH DEBUG")]
    public float touchDamping;

    [Header("MOVEMENT")]
    public float moveSpeed;
    public float torqueAmount;

    [Header("ROTATION")]
    public float rotationSpeed;



}
