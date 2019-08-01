using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WinterAI/Decisions/Alarm")]

public class AlarmDecision : Decision
{
    public override bool Decide(Winter controller) {


        bool imAlarmed = Alarm(controller);
        return imAlarmed;

    }

    private bool Alarm(Winter controller) {

        RaycastHit hit;

        Debug.DrawRay(controller.greyEyes.position, controller.greyEyes.forward.normalized * controller.status.lookSphereCastRadius, Color.green);

        if (controller.isAlarmed) {          // && Physics.SphereCast(controller.greyEyes.position, controller.status.lookSphereCastRadius, controller.greyEyes.forward, out hit, controller.status.visionRange) && hit.collider.tag == "Player"

            return true;

        } else {

            return false;

        }

       

    }

}
