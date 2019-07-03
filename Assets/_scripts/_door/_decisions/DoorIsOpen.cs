using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Doors/Decisions/DoorIsOpen")]

public class DoorIsOpen : DoorDecision{

    public override bool Decide(DoorControl controller) {

        bool doorIsOpen = IsOpen(controller);
        return doorIsOpen;

    }

    private bool IsOpen(DoorControl controller) {

        if(controller.doorOpen){

            return true;

        }   else {

            return false;
        }

    }


}
