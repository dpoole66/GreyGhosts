using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject {

    //JUST PASSING DATA AROUND:
    public abstract void Act(Winter controller);

}