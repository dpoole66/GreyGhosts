using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Doors/State")]

public class DoorState : ScriptableObject {

    public DoorAction[] actions;
    public DoorTransitions[] transitions;

    public void UpdateState(DoorControl controller){

        DoActions(controller);
        CheckTransitions(controller);

    }

    private void DoActions(DoorControl controller){

        for (int i = 0; i < actions.Length; i++) {

            actions[i].Act(controller);

        }

    }

    private void CheckTransitions(DoorControl controller){

        for (int i = 0; i < transitions.Length; i++) {

            bool decisionSucceeded = transitions[i].decision.Decide(controller);

            if (decisionSucceeded) {

                controller.TransitionToState(transitions[i].trueState);

            } else {

                controller.TransitionToState(transitions[i].falseState);

            }
        }

    }

}
