using System;
using UnityEngine;


namespace UnityStandardAssets.SceneUtils {
    public class AudreyTouchTarget : MonoBehaviour {

        public float surfaceOffset = 0.05f;
        public GameObject goTellAudrey;       

        public bool hitFloor = false;
        public bool hitWall = false;

        GameObject UI;

        private void Start() {

            UI = GameObject.Find("Canvas");

        }

        private void Update() {

            if (!Input.GetMouseButtonDown(0)) {
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            //LAYERMASK
            int FloorLayerIndex = 9;          
            int layerMask = 1 << FloorLayerIndex;

            if (!Physics.Raycast(ray, out hit)) {
                return;
            }


            hitFloor  = hit.collider.gameObject.tag == "floor";
            hitWall = hit.collider.gameObject.tag == "wall";

        

            if (goTellAudrey != null  && hitFloor) {

                transform.position = hit.point + hit.normal * surfaceOffset;

                UI.SetActive(true); 
                //goTellAudrey.SendMessage("FloorTarget", transform);   

            }

            if (goTellAudrey != null && hitWall) {

                UI.SetActive(false);
                transform.position = hit.point + hit.normal * surfaceOffset;
                goTellAudrey.SendMessage("LookAtWall", transform);

            }


        }
    }
}
