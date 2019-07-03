using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Doors/Decisions/OpenFromInside")]

public class DoorOpenInside : DoorDecision{

    public override bool Decide(DoorControl controller) {

        bool doorOpening = DoorOpen(controller);
        return doorOpening;

    }

    private bool DoorOpen(DoorControl controller) {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //LAYERMASK FLOOR
        int DoorTargetLayerIndex = 10;
        int layerMask = 1 << DoorTargetLayerIndex;

        if (Physics.Raycast(ray, out hit)) {

            if (hit.collider.tag == "doorInsideTarget") {

                Debug.Log("Hit Inside Door Target");

                return true;

            }
        } 
             return false;

    }
}
