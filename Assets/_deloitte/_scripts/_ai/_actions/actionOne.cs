using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Deloite_AI/Action/One")]

public class ActionOne : popAction {
    public override void Act(PopTimeController controller) {

        StateOne(controller);

    }

    private void StateOne(PopTimeController controller) {

        Debug.Log("STATE ONE ACTION");

        controller.ButtonGreen(controller.b1);

    }

}
