//// ClickToMove.cs
//using UnityEngine;
//using UnityEngine.AI;

//[RequireComponent(typeof(NavMeshAgent))]
//public class clicktomove : MonoBehaviour {

//    NavMeshAgent agent;   
//    RaycastHit hitInfo = new RaycastHit();
//    private AudCharacterControl audrey;

//    private void Start() {

//        audrey = GameObject.FindGameObjectWithTag("audrey").GetComponent<AudCharacterControl>();

//    }


//    void Update() {
//        if (Input.GetMouseButtonDown(0)) {

//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
//            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo, 15) && hitInfo.collider.tag == "plane")
//                audrey.target.transform.position = hitInfo.point;
//        }
//    }
//}