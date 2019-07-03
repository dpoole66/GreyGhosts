using System;

using UnityEngine;
using UnityEngine.EventSystems;


namespace UnityStandardAssets.SceneUtils
{
    public class PlaceTargetWithMouse : MonoBehaviour{

        public float surfaceOffset = 1.5f;
        public GameObject setTargetOn;

        // Update is called once per frame
        private void Update()

        {
            if (!Input.GetMouseButtonDown(0) || EventSystem.current.IsPointerOverGameObject() || EventSystem.current.currentSelectedGameObject != null) {

                return;

            }

            //if (Input.GetMouseButtonDown(0) || EventSystem.current.IsPointerOverGameObject()) {

            //    return;

            //}

            //if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) {

            //    return;

            //}

            //if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) {

            //    return;

            //}

           if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0)){

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                //LAYERMASK FLOOR
                LayerMask floor = LayerMask.GetMask("Floor");

                if (!Physics.Raycast(ray, out hit, floor) || hit.collider.tag != "floor") {

                    Debug.Log("Not Hitting Floor");
                    return;

                }

                if (Physics.Raycast(ray, out hit)) {

                    transform.position = hit.point + hit.normal * surfaceOffset;

                    if (setTargetOn != null) {

                        setTargetOn.SendMessage("SetTarget", transform);

                    }

                }

            }

           
          
        }
    }
}
