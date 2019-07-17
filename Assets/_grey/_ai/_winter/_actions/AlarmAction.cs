using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WinterAI/Actions/Alarm")]

public class AlarmAction : Action
{

    public override void Act(Winter controller) {

        Alarm(controller);

    }

    private void Alarm(Winter controller) {

        Debug.Log("ALARM ACTION");

        //LOOK AT PLAYER
        controller.lookTargeting.LookAtPlayer(-1f, 0.2f);

        //RING COLOR
        var main = controller.psRing.main;
        main.startColor = Color.red;
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
