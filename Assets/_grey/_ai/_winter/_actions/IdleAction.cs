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

        controller.anim.SetBool("idle", true);
        Debug.Log("I'm in the IDLE state");

    }

}
