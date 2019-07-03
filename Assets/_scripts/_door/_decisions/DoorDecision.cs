using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DoorDecision : ScriptableObject{

    public abstract bool Decide(DoorControl controller);

}
