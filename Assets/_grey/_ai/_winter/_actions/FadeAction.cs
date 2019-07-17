using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WinterAI/Actions/Fade")]

public class FadeAction : Action
{
    public override void Act(Winter controller) {

        Fade(controller);

    }

    private void Fade(Winter controller) {

        Debug.Log("FADE ACTION");

        //controller.StartFade();

    }

}
