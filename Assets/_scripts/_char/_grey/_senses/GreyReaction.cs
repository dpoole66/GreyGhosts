using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreyReaction : MonoBehaviour
{

    public  Animator anim;

    //HAND SWITCHING FROM CONTACT ANGLE
    public bool left;
    public bool right;
    public bool steppingBack;



    private void Update() {

        anim.SetBool("stepback", steppingBack);

    }


}
