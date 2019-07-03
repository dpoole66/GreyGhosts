//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[CreateAssetMenu(menuName = "Doors/Decisions/Open")]

//public class DoorOpen : DoorDecision{

    //public override System.Boolean Decide(DoorControl controller) {

    //    bool doorOpening = Open(controller);
    //    return doorOpening;

    //}

    //private bool Open(DoorControl controller){

    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;

    //    //LAYERMASK FLOOR
    //    int DoorTargetLayerIndex = 10;
    //    int layerMask = 1 << DoorTargetLayerIndex;

    //    if (Physics.Raycast(ray, out hit)) {

    //            if (hit.collider.tag == "doorInsideTarget") {

    //                Debug.Log("Hit Inside Door Target");

    //                return true;

    //            }  else {

    //                return false;

    //             }

    //    }

    //}

//}
