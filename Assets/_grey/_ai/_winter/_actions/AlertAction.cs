using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WinterAI/Actions/Alert")]

public class AlertAction : Action
{

    public override void Act(Winter controller) {

        Alert(controller);

    }

    private void Alert(Winter controller) {

        Debug.Log("ALERT ACTION");

        //LOOK AT PLAYER
        controller.lookTargeting.LookAtPlayer(0.5f, 0.08f);

        //RING COLOR
        var main = controller.psRing.main;
        main.startColor = Color.yellow;
        //main.loop = true;

        if (controller.isMoveing) {

            controller.psRing.Play();

        }

        if (controller.isIdle) {

            //controller.psRing.Stop();
            controller.psBody.Play();
            controller.psTop.Play();

        }
        //Debug.Log("I'm in the Alert state ACTION and SEEING");

    }


}
