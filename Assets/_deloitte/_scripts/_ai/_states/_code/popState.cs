using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Deloite_AI/State")]

public class popState : ScriptableObject {

    public popAction[] actions;
    public popTransition[] transitions;
    public Color sceneGizmoColor = Color.grey;

    public void UpdateState(PopTimeController popController) {
        DoActions(popController);
        CheckTransitions(popController);
    }

    private void DoActions(PopTimeController popController) {

        for (int i = 0; i < actions.Length; i++) {
            actions[i].Act(popController);
        }

    }

    private void CheckTransitions(PopTimeController popController) {

        for (int i = 0; i < transitions.Length; i++) {

            bool decisionSucceeded = transitions[i].decision.Decide(popController);

            if (decisionSucceeded) {

                popController.TransitionToState(transitions[i].trueState);

            } else {

                popController.TransitionToState(transitions[i].falseState);

            }

        }
    }


}