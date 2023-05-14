using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeholder_Movement : MonoBehaviour
{
    public float power = 50f;
    public float torque = 25f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    void FixedUpdate()
    {
        ProcessInput();
    }

    void ProcessInput()
    {
        float vt = Input.GetAxis("Vertical");
        Vector3 force = new Vector3(0f, 0f, vt * power);
        rb.AddRelativeForce(force);

        float hz = Input.GetAxis("Horizontal");
        Vector3 rforce = new Vector3(0f, hz * torque, 0f);
        rb.AddRelativeTorque(rforce);
    }
}
