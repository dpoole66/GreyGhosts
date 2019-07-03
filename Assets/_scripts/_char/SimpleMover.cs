//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//[RequireComponent(typeof(NavMeshAgent))]

//public class SimpleMover : MonoBehaviour{

//    RaycastHit hitInfo = new RaycastHit();
//    NavMeshAgent agent;

//    void Start() {
//        agent = GetComponent<NavMeshAgent>();
//    }
//    void Update() {
//        if (Input.GetMouseButtonDown(0)) {
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo) && hitInfo.collider.tag == "floor") ;
//                agent.destination = hitInfo.point;
//        }
//    }
//}
