using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WinterAI/Actions/Looking")]

public class LookingAction : Action {

    public override void Act(Winter controller) {

        Looking(controller);        

    }

    private void Looking(Winter controller) {

        controller.lookTargeting.LookAtPlayer(0.5f, 0.08f);
        //Debug.Log("I'm in the LOOKING state");

    }

}
