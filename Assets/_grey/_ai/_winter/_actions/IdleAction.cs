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

        Debug.Log("AWARE ACTION");

        //RING COLOR
        var main = controller.psRing.main;
        main.startColor = Color.blue;
        //main.loop = true;

        ////DO WHILE
        //do {

        //    controller.psRing.Play();
        //    controller.psBody.Play();
        //    controller.psTop.Play();

        //} while (controller.isMoveing == true);

        if (controller.isMoveing) {

            controller.psRing.Play();

        }

        if (controller.isIdle) {

            //controller.psRing.Stop();
            controller.psBody.Play();
            controller.psTop.Play();

        }

    }

}
