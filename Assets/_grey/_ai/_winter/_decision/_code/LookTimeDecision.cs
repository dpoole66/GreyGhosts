using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WinterAI/Decisions/LookTimer")]

public class LookTimeDecision : Decision {

    public override bool Decide(Winter controller) {

        bool lookAway = LookAway(controller);
        return lookAway;

    }

    public bool LookAway(Winter controller) {

        if (!controller.timerStarted) {

            return false;

        } else {

            controller.lookTargeting.LookAroundIdly();
            Debug.Log("Looking away: ");
            return true;

        }
    }
}
