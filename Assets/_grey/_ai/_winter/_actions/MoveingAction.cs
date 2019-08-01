using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "WinterAI/Actions/Moving")]

public class MoveingAction : Action
{

    public override void Act(Winter controller) {

        Moveing(controller);

    }

    private void Moveing(Winter controller){

        //Debug.Log("MOVEING ACTION NAV");

        //controller.agent.destination = controller.debugMoveTo.position;
        //controller.agent.updatePosition = true;
        //controller.agent.updateRotation = true;
        //controller.agent.isStopped = false;

        //RB
        //Get look to rotation
        Quaternion DestRot = Quaternion.LookRotation(controller.target.transform.position - controller.transform.position, Vector3.up);

        //Calc smooth rotate
        Quaternion smoothRot = Quaternion.Slerp(controller.transform.rotation, DestRot, 1f - (Time.deltaTime * controller.status.rotationSpeed));

        //Update Rotation
        controller.transform.rotation = smoothRot;

        controller.anim.SetFloat("vert", smoothRot.y);

        if(controller.goTo){

            if (smoothRot.y < -0.1f) {

                controller.anim.SetBool("turnLeft", true);
                controller.anim.SetBool("turnRight", false);

            }

            if (smoothRot.y > 0.1f) {

                controller.anim.SetBool("turnLeft", false);
                controller.anim.SetBool("turnRight", true);


            }

        } else {

            controller.anim.SetBool("turnLeft", false);
            controller.anim.SetBool("turnRight", false);
            smoothRot.y = 0f;

        }


    } 



}
