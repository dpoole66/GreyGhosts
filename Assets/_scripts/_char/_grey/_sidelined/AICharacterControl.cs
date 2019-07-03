using System;
using UnityEngine;

public class AICharacterControl : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
    public AudPersonCharacter character { get; private set; } // the character we are controlling
    public Transform target;                                    // target to aim for
    Animator anim;


    private void Start()
    {
        // get the components on the object we need ( should not be null due to require component so no need to check )
        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        character = GetComponent<AudPersonCharacter>();
        anim = GetComponent<Animator>();
	    agent.updateRotation = false;
	    agent.updatePosition = true;
    }


    private void Update()
    {
        if (target != null)
            agent.SetDestination(target.position);

        if (agent.remainingDistance > agent.stoppingDistance) {
            character.Move(agent.desiredVelocity, false, false);
            anim.SetBool("idle", false);
        } else {
            character.Move(Vector3.zero, false, false);
            anim.SetBool("idle", true);
        }
    }


    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
