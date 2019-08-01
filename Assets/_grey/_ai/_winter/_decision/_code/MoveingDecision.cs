using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WinterAI/Decisions/Moveing")]

public class MoveingDecision : Decision
{

    public override bool Decide(Winter controller) {

        bool imMoveing = Moveing(controller);
        return imMoveing;

    }

    private bool Moveing(Winter controller) {


        if (controller.goTo ){

            Debug.Log("Move Decision");
            //controller.isMoveing = true;
            //controller.agent.isStopped = false; ;
            return true;

        } else {

            //controller.agent.isStopped = true;
            //controller.isMoveing = false;
            return false;

        }

    }



}
