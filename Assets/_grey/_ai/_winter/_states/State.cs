using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WinterAI/States")]

public class State : ScriptableObject
{

    public Action[] actions;
    public Transition[] transitons;
    public Color sceneGizmoColor = Color.grey;

    public void UpdateState(Winter controller)
    {

        DoActions(controller);
        CheckTransitions(controller);

    }

    private void DoActions(Winter controller)
    {

        for(int i = 0; i < actions.Length; i++){

                actions[i].Act(controller);

        }

    }

    private void CheckTransitions(Winter controller)
    {

        for(int i = 0; i < transitons.Length; i++){

                 bool decisionSucceeded = transitons[i].decision.Decide(controller);

                 if(decisionSucceeded){

                    controller.TransitionToState(transitons[i].trueState);

                 } else{

                    controller.TransitionToState(transitons[i].falseState);

                 }

        }

    }
  
}
