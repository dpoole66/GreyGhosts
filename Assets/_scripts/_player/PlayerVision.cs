using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVision : MonoBehaviour
{

    //RANGEFINDING
    PlayerController Player;
    Transform PlayerEyes;
    Transform GreyEyes;         
    GreyAwareness Grey;

    void Start() {

        Player = GetComponent<PlayerController>();
        Grey = GameObject.FindGameObjectWithTag("grey").GetComponent<GreyAwareness>();

    }


    //COROUTINE FOR VISION/SPHERECASTING
    public IEnumerator Vision() {

        if (Player.isLooking) {

            RaycastHit hit;

            if (Physics.SphereCast(Player.playerEyesCur.transform.position, Player.sphereScale, Player.playerEyesCur.forward, out hit, Player.visionRange)) {

                Player.hitPoint = hit.point;


                if (hit.collider.tag == "grey") {

                    Debug.Log("I see The Grey Lady!");
                    Player.iSeeSomething = true;
                    //TOGGLE GREY BOOL
                    Grey.isBeingLookedAt = true;

                }

            } else {

                Debug.Log("I don't see anything!");
                Player.iSeeSomething = false;
                //TOGGLE GREY BOOL
                Grey.isBeingLookedAt = false;

            }

            yield return null;


        }


    }
}
