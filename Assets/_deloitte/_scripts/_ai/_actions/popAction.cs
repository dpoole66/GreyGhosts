using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class popAction : ScriptableObject {

    public abstract void Act(PopTimeController controller);

}