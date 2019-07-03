using System.Collections;
using System.Collections.Generic;
using UnityEngine;             

public class DoorControl : MonoBehaviour{

    public DoorState currentState;
    public DoorState remainState;
    //DOOR STATES
    public bool doorOpen;
    public bool doorIsOpening;
    public bool doorClosed;
    public bool doorIsCloseing;
    //DOOR DATA
    public DoorStats stats;
    [HideInInspector] public float stateTimeElapsed;
    //NAVMESHAGENT POSITIONS
    public Transform doorInsideOpen;
    public Transform doorOutsideOpen;
    //MESSAGE HIT
    private GameObject goTellAudrey;

    private void Start() {

        goTellAudrey = GameObject.FindGameObjectWithTag("grey");
        doorInsideOpen = GetComponent<Transform>();

    }

    private void Update() {

        currentState.UpdateState(this);

    }

    public void TransitionToState(DoorState nextState) {

        if (nextState != remainState) {
            currentState = nextState;
            OnExitState();
        }

    }

    public bool CheckIfCountDownElapsed(float duration) {

        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);

    }

    private void OnExitState() {

        stateTimeElapsed = 0;

    }

    //DOOR
    public void DoorInsideOpen(){

        goTellAudrey.SendMessage("DoorTarget", this.doorInsideOpen.position);

    }

}
