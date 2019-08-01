using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class popDecision : ScriptableObject {

    public abstract bool Decide(PopTimeController controller);

}