using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DoorAction : ScriptableObject{
  
      public abstract void Act(DoorControl controller);

}
