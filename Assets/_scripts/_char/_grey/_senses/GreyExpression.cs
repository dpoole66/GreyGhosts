using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreyExpression : MonoBehaviour
{

    //GREY ANXIETY
    [Header("GREY COMPONENTS ")]
    public GreyAnxiety greyAnxiety;
    public GreyAwareness aware;

    //BLEND SETTINGS
    [Header("IK BLEND ")]       
    public float step = 1f;
    float currentAnxiety;

    //FRIEND OR FOE
    [Header("FRIEND OR FOE ")]
    public bool isFriend;
    public bool notTrusted;
    public float anxietyWeight;
    float anxietyRate;
    float anxiety;

    //GET SKINNED MESH RENDERERS FOR BODY PARTS
    int blendShapeCount;

    //BODY
    [Header("BODY MESH")]
    public SkinnedMeshRenderer bodyskinrend;
    public Mesh bodyskinmesh;

    //EYELASHES
    [Header("EYELASH MESH")]
    public SkinnedMeshRenderer eyelashskinrend;
    public Mesh eyelashskinmesh;

    private void Start() {

        //bodyskinrend = GameObject.FindGameObjectWithTag("ikBody").GetComponent<SkinnedMeshRenderer>();
        //bodyskinmesh = GameObject.FindGameObjectWithTag("ikBody").GetComponent<SkinnedMeshRenderer>().sharedMesh;
        //eyelashskinrend = GameObject.FindGameObjectWithTag("ikEyeLashes").GetComponent<SkinnedMeshRenderer>();
        //eyelashskinmesh = GameObject.FindGameObjectWithTag("ikEyeLashes").GetComponent<SkinnedMeshRenderer>().sharedMesh;

        blendShapeCount = bodyskinmesh.blendShapeCount;

    }

    public void Response(){

        //StartCoroutine(FriendResponse());
        StartCoroutine(EyesExpression());
        StartCoroutine(MouthExpression());

    }

    public IEnumerator EyesExpression() {

        while (true) {

            anxietyWeight = greyAnxiety.anxiety;
            anxietyRate = greyAnxiety.anxietyRate;
            anxiety = greyAnxiety.anxiety;
            notTrusted = aware.notTrusted;

            if (anxietyRate <= anxiety) {

                //EYES                    
                bodyskinrend.SetBlendShapeWeight(12, anxietyRate);
                bodyskinrend.SetBlendShapeWeight(13, anxietyRate);
                eyelashskinrend.SetBlendShapeWeight(12, anxietyRate);
                eyelashskinrend.SetBlendShapeWeight(13, anxietyRate);
                //BROWS
                bodyskinrend.SetBlendShapeWeight(2, anxietyRate);
                bodyskinrend.SetBlendShapeWeight(3, anxietyRate);
                greyAnxiety.anxietyRate += step;

            } else {

                //EYES                    
                bodyskinrend.SetBlendShapeWeight(12, anxietyRate);
                bodyskinrend.SetBlendShapeWeight(13, anxietyRate);
                eyelashskinrend.SetBlendShapeWeight(12, anxietyRate);
                eyelashskinrend.SetBlendShapeWeight(13, anxietyRate);
                //BROWS
                bodyskinrend.SetBlendShapeWeight(2, anxietyRate);
                bodyskinrend.SetBlendShapeWeight(3, anxietyRate);
                greyAnxiety.anxietyRate -= step;

            }


            yield return null;

        }

    }

    public IEnumerator MouthExpression() {

        while (true) {

            anxietyWeight = greyAnxiety.anxiety;
            anxietyRate = greyAnxiety.anxietyRate;
            anxiety = greyAnxiety.anxiety;
            notTrusted = aware.notTrusted;

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

            anxietyWeight = greyAnxiety.anxiety;
            notTrusted = aware.notTrusted;

            //NOT TRUSTED   
            if (notTrusted) {

                //anxietyRamp = Random.Range(35f, 40f * Time.deltaTime);
                //greyAnxiety.anxietyRate = 65f;
                //StartCoroutine(AnxietyPhase(30f, 45f));

                if (greyAnxiety.anxietyRate <= greyAnxiety.anxiety) {

                    bodyskinrend.SetBlendShapeWeight(43, greyAnxiety.anxiety);
                    bodyskinrend.SetBlendShapeWeight(44, greyAnxiety.anxiety);
                    greyAnxiety.anxietyRate += step;

                }
                if (greyAnxiety.anxietyRate == greyAnxiety.anxiety) {

                    bodyskinrend.SetBlendShapeWeight(43, greyAnxiety.anxiety);
                    bodyskinrend.SetBlendShapeWeight(44, greyAnxiety.anxiety);
                    greyAnxiety.anxietyRate -= step;

                }

            }

            if (!notTrusted) {

                greyAnxiety.anxietyRate = -5f;
                //StartCoroutine(AnxietyPhase(-10f, 5f));

                if (greyAnxiety.anxietyRate <= greyAnxiety.anxiety) {

                    bodyskinrend.SetBlendShapeWeight(43, greyAnxiety.anxietyRate);
                    bodyskinrend.SetBlendShapeWeight(44, greyAnxiety.anxietyRate);
                    greyAnxiety.anxietyRate += step;


                }
                if (greyAnxiety.anxietyRate == greyAnxiety.anxiety) {

                    bodyskinrend.SetBlendShapeWeight(43, greyAnxiety.anxietyRate);
                    bodyskinrend.SetBlendShapeWeight(44, greyAnxiety.anxietyRate);
                    greyAnxiety.anxietyRate -= step;

                }

            }

            yield return null;

        }

    }
}
