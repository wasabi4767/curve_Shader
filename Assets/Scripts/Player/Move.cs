using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody rb;
    float speed = 3.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody> ();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.W)){
            rb.velocity = transform.forward*speed;
        }
        if(Input.GetKey(KeyCode.S)){
            rb.velocity = -transform.forward*speed;
        }
        if(Input.GetKey(KeyCode.D)){
            rb.velocity = transform.right*speed;
        }
        if(Input.GetKey(KeyCode.A)){
            rb.velocity = -transform.right*speed;
        }
    }
}
