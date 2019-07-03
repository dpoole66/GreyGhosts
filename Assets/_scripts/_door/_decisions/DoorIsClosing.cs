using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Doors/Decisions/DoorIsCloseing")]

public class DoorIsClosing : DoorDecision{

    public override System.Boolean Decide(DoorControl controller) {

        bool doorIsCloseing = IsCloseing(controller);
        return doorIsCloseing;

    }

    private bool IsCloseing(DoorControl controller){

        if(controller.doorIsCloseing){

                return true;

        } else{

                return false;

        }

    }

}
