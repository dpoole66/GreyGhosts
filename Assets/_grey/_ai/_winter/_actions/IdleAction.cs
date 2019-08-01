using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WinterAI/Actions/Idle")]

public class IdleAction : Action
{

    public override void Act(Winter controller) {

        Idle(controller);

    }           
    
    private void Idle(Winter controller){

        Debug.Log("IDLE ACTION");

        ////STOP AGENT
        //if(!controller.agent.pathPending && controller.agent.remainingDistance <= controller.agent.stoppingDistance){

        //    controller.agent.destination = controller.agent.destination;
        //    controller.agent.updatePosition = false;
        //    controller.agent.updateRotation = false;
        //    controller.agent.isStopped = true;

        //}


    }

}
