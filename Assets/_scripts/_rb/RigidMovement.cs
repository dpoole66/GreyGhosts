using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidMovement : MonoBehaviour
{
    //PHYSICS
    public float torqueAmount = 100.0f;
    //COMPONENTS
    public Rigidbody rb;
    public Animator anim;
    //BOOLS
    public bool moving = false;


    void FixedUpdate() {

        float h = Input.GetAxis("Horizontal") * torqueAmount * Time.deltaTime;
        float v = Input.GetAxis("Vertical") * torqueAmount * Time.deltaTime;
        float y = rb.angularVelocity.magnitude * torqueAmount * Time.deltaTime;

        float dampv = v * 5f * Time.deltaTime;
        float damph = h * 20f * Time.deltaTime;

        anim.SetFloat("vert", dampv);
        anim.SetFloat("hori", damph);

        rb.AddForce(transform.forward * dampv, ForceMode.Impulse);
        transform.Rotate(transform.up * damph, Space.Self);

        moving = dampv >= 0.1f || dampv <= -0.1f;
        anim.SetBool("moveing", moving);
    }

}
