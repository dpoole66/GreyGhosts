using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Deloite_AI/Decision/One")]

public class decisionOne : popDecision{

    public override bool Decide(PopTimeController controller) {


        bool decisionOne = DecideOne(controller);
        return decisionOne;

    }

    private bool DecideOne(PopTimeController controller) {

        if (controller.decision1 == true) {        

            return true;

        } else {

            return false;

        }



    }

}
