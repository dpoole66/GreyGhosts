using System;
using UnityEngine;


namespace UnityStandardAssets.SceneUtils {
    public class ikTouchInput : MonoBehaviour {

        public float surfaceOffset = 0.05f;
        public GameObject goTellAudrey;                           
        public bool hitFloor = false;
        public bool hitWall = false;    


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

            Debug.Log("Hit Point: " + hit.point);
            //GET TOUCH INPUT POINT FOR NAV AND LOOK
            Transform LookHere = new GameObject().transform;
            LookHere.transform.position = hit.point;
            goTellAudrey.SendMessage("LookAtThisPoint", LookHere);

            hitFloor = hit.collider.gameObject.tag == "floor";
            hitWall = hit.collider.gameObject.tag == "wall";

  

        }
    }
}
