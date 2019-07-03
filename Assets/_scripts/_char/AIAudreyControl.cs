using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson {
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof(Audrey3PCharacter))]
    public class AIAudreyControl : MonoBehaviour {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }
        public Audrey3PCharacter character { get; private set; }
        public Transform target;       
        //DOOR RELATION BOOL
        public bool insideDoor;
        public bool outsideDoor;


        private void Start() {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<Audrey3PCharacter>();

            agent.updateRotation = false;
            agent.updatePosition = true;
        }


        private void Update() {

            if (target != null)
                agent.SetDestination(target.position);

            if (agent.remainingDistance > agent.stoppingDistance)
                character.Move(agent.desiredVelocity, false, false);
            else
                character.Move(Vector3.zero, false, false);

        }


        public void FloorTarget(Transform target) {

            this.target = target;

        }


        public void DoorTarget(Transform moveTo) {

            this.target = moveTo;
            Debug.Log("Door Target: " + moveTo.position);
            target.transform.rotation = Quaternion.RotateTowards(character.transform.rotation, target.rotation, 300f * Time.deltaTime);

        }

        public void DoorInsideOutside(int inOut) {

            if (inOut == 1) {

                insideDoor = true;
                outsideDoor = !insideDoor;                                           

            }

            if (inOut == 2) {                                                                                                                                                

                insideDoor = false;
                outsideDoor = !insideDoor;                   

            }

            Debug.Log("Door IN/OUT: " + inOut);

        }

    }

   
}
