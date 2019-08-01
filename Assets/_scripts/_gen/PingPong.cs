using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPong : MonoBehaviour
{

    public float speed = 3f;

    void Update() {
        // Set the x position to loop between 0 and 3
        transform.position = new Vector3(Mathf.PingPong(Time.time, speed), transform.position.y, transform.position.z);
    }
}

