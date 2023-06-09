using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet_Gravity : MonoBehaviour
{
    public Transform gravityTarget;

    public float gravity = 9.81f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    void FixedUpdate()
    {
        ProcessGravity();
    }

    void ProcessGravity()
    {
        Vector3 diff = transform.position - gravityTarget.position;
        rb.AddForce(-diff.normalized * gravity * (rb.mass));
        Debug.DrawRay(transform.position, diff.normalized, Color.red);
    }
}
