using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Doors/Decisions/DoorIsClosed")]

public class DoorIsClosed : DoorDecision {

    public override bool Decide(DoorControl controller) {

        bool doorIsClosed = IsClosed(controller);
        return doorIsClosed;

    }

    private bool IsClosed(DoorControl controller) {

        if (controller.doorClosed) {

            return true;

        } else {

            return false;
        }

    }

}
