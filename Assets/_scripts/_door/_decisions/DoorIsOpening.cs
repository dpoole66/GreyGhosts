using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Doors/Decisions/DoorIsOpening")]

public class DoorIsOpening : DoorDecision{

    public override bool Decide(DoorControl controller) {

        bool doorIsOpening = IsOpening(controller);
        return doorIsOpening;

    }

    private bool IsOpening(DoorControl controller) {

        if (controller.doorIsOpening) {

            return true;

        } else {

            return false;
        }

    }


}
