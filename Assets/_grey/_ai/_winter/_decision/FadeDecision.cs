using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WinterAI/Decisions/Fade")]

public class FadeDecision : Decision
{
    public override bool Decide(Winter controller) {


        bool fade = Fadeing(controller);
        return fade;

    }

    private bool Fadeing(Winter controller) {


        Debug.DrawRay(controller.greyEyes.position, controller.greyEyes.forward.normalized * controller.status.lookSphereCastRadius, Color.grey);

        if (controller.isFadeing) {          // && Physics.SphereCast(controller.greyEyes.position, controller.status.lookSphereCastRadius, controller.greyEyes.forward, out hit, controller.status.visionRange) && hit.collider.tag == "Player"

            return true;

        } else {

            return false;

        }



    }
}
