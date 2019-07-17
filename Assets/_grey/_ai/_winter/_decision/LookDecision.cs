using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WinterAI/Decisions/Looking")]

public class LookDecision : Decision
{

    public override bool Decide(Winter controller){


            bool playerVisible = Look(controller);
            return playerVisible;

    }

    private bool Look(Winter controller){

        RaycastHit hit;

        Debug.DrawRay(controller.greyEyes.position, controller.greyEyes.forward.normalized * controller.status.lookSphereCastRadius, Color.green);

        if (Physics.SphereCast(controller.greyEyes.position, controller.status.lookSphereCastRadius, controller.greyEyes.forward, out hit, controller.status.visionRange) && hit.collider.tag == "Player") {

            controller.isSeeing = true;
            Debug.Log("I'm seeing. " + hit.collider.tag.ToString());

            return true;

        }    else{

            controller.isSeeing = false;
            return false;

        }

    }
   
}
