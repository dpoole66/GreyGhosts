using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WinterAI/Decisions/Alert")]

public class AlertDecision : Decision
{

    public override bool Decide(Winter controller) {


        bool imAlerted = Alert(controller);
        return imAlerted;

    }

    private bool Alert(Winter controller) {

        RaycastHit hit;

        Debug.DrawRay(controller.greyEyes.position, controller.greyEyes.forward.normalized * controller.status.lookSphereCastRadius, Color.yellow);

        if (Physics.SphereCast(controller.greyEyes.position, controller.status.lookSphereCastRadius, controller.greyEyes.forward, out hit, controller.status.visionRange) && hit.collider.tag == "Player") {

            controller.isAlert = true;    
            return true;

        } else  {

            controller.isAlert = false;
            return false;

        }

    }

}
