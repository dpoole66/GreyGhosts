using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreySenses : MonoBehaviour{

    //GET SKINNED MESH RENDERERS FOR BODY PARTS
    int blendShapeCount;

    //BODY
    SkinnedMeshRenderer bodyskinrend;
    Mesh bodyskinmesh;

    //EYELASHES
    SkinnedMeshRenderer eyelashskinrend;
    Mesh eyelashskinmesh;
    
     //TRANSFORM FOR VISION CASTING
    //[HideInInspector]
    //public Transform playerEye;

    ////ALERT SETTINGS  
    //[Header("LOOK  AT RANGE: ")]
    //public float range;
    //public float rangeSquareMag;
    //[HideInInspector]
    //public Transform greyEye;
    ////HEARING RANGE
    //[Header("HEARING RANGE: ")]
    //[Range(0f, 15f)]
    //public float awareRange = 2f;
    //AWARENESS 
    [Header("AWARENESS SWITCHING:")]
    GreyAwareness awareness;

    [Header("ANXIETY: ")]
    [Range(-45f, 45f)]
    public float anxiety = 0f;

    //BLEND SETTINGS
    [Header("IK BLEND: ")]
    public float anxietyRate = 10f;
    public float step = 1f;
    float currentAnxiety;

    //FRIEND OR FOE
    [Header("FRIEND OR FOE: ")]
    public bool isFriend;

    //TRUST 
    [Header("TRUST LEVEL: ")]
    public float trustLevel;
    [Header("TRUST TIMER: ")]
    public int seconds = 0;
    public int minutes = 0;
    public int hours = 0;
    public int days = 0;
    public bool pause = false;
    [Header("TRUST STATES:")]        
    public bool trustAchieved = false;
    public bool trustLost = false;

    //VIOLATIONS
    //COUNTER   
    [Header("THERE ARE CURRENT VIOLATIONS:")]
    public bool existingViolations;
    [Header("VIOLATIONS:")]
    public bool violation;
    public int violations;
    public int violationTime;
    public int vioCoolDown;
    //COUNTER   
    [Header("VIOLATION TIMER:")]
    public bool countStarted;
    public bool countComplete;


    private void Awake() {

        bodyskinrend = GameObject.FindGameObjectWithTag("ikBody").GetComponent<SkinnedMeshRenderer>();
        bodyskinmesh = GameObject.FindGameObjectWithTag("ikBody").GetComponent<SkinnedMeshRenderer>().sharedMesh;
        eyelashskinrend = GameObject.FindGameObjectWithTag("ikEyeLashes").GetComponent<SkinnedMeshRenderer>();
        eyelashskinmesh = GameObject.FindGameObjectWithTag("ikEyeLashes").GetComponent<SkinnedMeshRenderer>().sharedMesh;

        awareness = GameObject.FindGameObjectWithTag("grey").GetComponent<GreyAwareness>();

        trustLevel = 0f;
        blendShapeCount = bodyskinmesh.blendShapeCount;


    }

    private void Start() {

        StartCoroutine(EyesExpression());
        StartCoroutine(MouthExpression());
        StartCoroutine(FriendResponse());
        StartCoroutine(Trust());
        violation = false;
        violations = 0;
        existingViolations = false;

    }

   

    //TALK TO GREY AWARENESS
    public void Violation(){

        StartCoroutine(ViolationTimer(seconds));

    }

    public void ViolationCool() {

        StartCoroutine(ViolationCoolDown());

    }


    public IEnumerator EyesExpression() {

        while (true) {

            if (anxietyRate <= anxiety) {

                //EYES                    
                bodyskinrend.SetBlendShapeWeight(12, anxietyRate);
                bodyskinrend.SetBlendShapeWeight(13, anxietyRate);
                eyelashskinrend.SetBlendShapeWeight(12, anxietyRate);
                eyelashskinrend.SetBlendShapeWeight(13, anxietyRate);
                //BROWS
                bodyskinrend.SetBlendShapeWeight(2, anxietyRate);
                bodyskinrend.SetBlendShapeWeight(3, anxietyRate);
                anxietyRate += step;

            } else {

                //EYES                    
                bodyskinrend.SetBlendShapeWeight(12, anxietyRate);
                bodyskinrend.SetBlendShapeWeight(13, anxietyRate);
                eyelashskinrend.SetBlendShapeWeight(12, anxietyRate);
                eyelashskinrend.SetBlendShapeWeight(13, anxietyRate);
                //BROWS
                bodyskinrend.SetBlendShapeWeight(2, anxietyRate);
                bodyskinrend.SetBlendShapeWeight(3, anxietyRate);
                anxietyRate -= step;

            }


            yield return null;

        }

    }

    public IEnumerator MouthExpression() {

        while (true) {

            //MOUTH            
            if (anxietyRate <= anxiety) {

                bodyskinrend.SetBlendShapeWeight(14, anxietyRate);
                bodyskinrend.SetBlendShapeWeight(15, anxietyRate);
                anxietyRate += step;

            } else {

                bodyskinrend.SetBlendShapeWeight(14, anxietyRate);
                bodyskinrend.SetBlendShapeWeight(15, anxietyRate);
                anxietyRate -= step;

            }

            yield return null;

        }

    }

    public IEnumerator FriendResponse() {

        while (true) {

            //SMILE   
            if (!isFriend) {

                //anxietyRamp = Random.Range(35f, 40f * Time.deltaTime);
                anxietyRate = 35f;
                //StartCoroutine(AnxietyPhase(30f, 45f));

                if (anxietyRate <= anxiety) {

                    bodyskinrend.SetBlendShapeWeight(43, anxietyRate);
                    bodyskinrend.SetBlendShapeWeight(44, anxietyRate);
                    anxietyRate += step;

                }
                if (anxietyRate == anxiety) {

                    bodyskinrend.SetBlendShapeWeight(43, anxietyRate);
                    bodyskinrend.SetBlendShapeWeight(44, anxietyRate);
                    anxietyRate -= step;

                }

            }

            if (isFriend) {

                anxietyRate = -5f;
                //StartCoroutine(AnxietyPhase(-10f, 5f));

                if (anxietyRate <= anxiety) {

                    bodyskinrend.SetBlendShapeWeight(43, anxietyRate);
                    bodyskinrend.SetBlendShapeWeight(44, anxietyRate);
                    anxietyRate += step;

                }
                if (anxietyRate == anxiety) {

                    bodyskinrend.SetBlendShapeWeight(43, anxietyRate);
                    bodyskinrend.SetBlendShapeWeight(44, anxietyRate);
                    anxietyRate -= step;

                }

            }

            yield return null;

        }

    }

    //VIOLATION TIMER
    public IEnumerator ViolationTimer(int seconds){

        while (seconds > 0) {

            Debug.Log("Countdown =  " + seconds);
            countStarted = true;
            countComplete = false;
            //violation = false;
            //pause = true;
            yield return new WaitForSeconds(1);
            seconds--;

        }

        if (seconds == 0) {

            //Debug.Log("Countdown =  " + seconds);
            countStarted = false;
            countComplete = true;
            violation = true;
            violations += 1;
            existingViolations = true;

            //StartCoroutine(ViolationCoolDown());

            yield break;

        }

    }

    public IEnumerator ViolationCoolDown(){

        if (existingViolations && !violation) {

            while(vioCoolDown > 0){

                    int coolSeconds = vioCoolDown;
                    yield return new WaitForSeconds(1);
                    coolSeconds--;

             }

             if(vioCoolDown == 0){

                //violation = false;
                existingViolations = false;
                yield break;

            }

        }

    }


    //VIOLATION COUNTDOWN

    public IEnumerator Counter(float seconds) {

        float countdown = seconds;
        Debug.Log("Countdown =  " + seconds);
        yield return new WaitForSeconds(seconds);

        while (countdown > 0f) {

            Debug.Log("Countdown");
            countStarted = true;
            countComplete = false;
            yield return new WaitForSeconds(1);
            countdown--;
            Debug.Log(countdown);

            if (countdown == 0f) {

                Debug.Log("Breaking from Countdown");
                countStarted = false;
                countComplete = true;
                //violations += 1;
                //yield break;
                countdown = seconds;

            }

            yield break;

        }

    }

    //TRUST INDEX
    public IEnumerator Trust(){

        while (true) {

            //TRUST LEVEL
            trustLevel = Mathf.Clamp(trustLevel, 0f, 100f);
            trustAchieved = trustLevel >= 100f;
            trustLost = violations >= 3f;

            //FRIEND ONLY IF NO CURRENT VIOLATIONS
            isFriend = trustAchieved && !existingViolations;

            //VIOLATIONS
            existingViolations = violations >= 1;

            //PAUSE TIMER
            pause = violation;

            //PROGRESSION
            if (!pause) {

                float tick = 0f;
                tick += Time.deltaTime;

                if(tick > 1f){

                    seconds += 1;
                    tick -= 1f;

                }

                //seconds += Time.deltaTime;
                trustLevel += Time.deltaTime;

            }

            if(trustAchieved){

                violations = 0;

            }

            if (trustLost) {

                minutes = 0;
                trustLevel = 0f;
                //trustLost = false;

            }

            if (seconds > 6) {

                minutes += 1;
                seconds = 0;

            }

            if (minutes > 60) {

                hours += 1;
                minutes = 0;

            }

            if (hours > 24) {

                days += 1;
                hours = 0;

            }


            yield return null;

        }

    }


}
