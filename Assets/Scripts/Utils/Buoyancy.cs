using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Buoyancy : MonoBehaviour
{
    public Transform[] floaters;
    public float underWaterDrag = 3f;
    public float underWaterAngularDrag = 1f;
    public float airDrag = 0f;
    public float airAngularDrag = 0.05f;
    public float floatingPower = 15f;
    public float waterHeight = 0f;
    Rigidbody RB;
    bool underwater;
    int floatersunderWater;
    public float waterSurfaceTension = 0.5f;

    // private float waterExitTime;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        floatersunderWater = 0;
        for(int i = 0; i < floaters.Length; i++)
        {
            float diff = floaters[i].position.y - waterHeight;
            if (diff < 0)
            {
                RB.AddForceAtPosition(Vector3.up * floatingPower * Mathf.Abs(diff), floaters[i].position, ForceMode.Force);
                floatersunderWater += 1;
                if (!underwater)
                {
                    underwater = true;
                    SwitchState(true);
                }
            }
        }
        if (underwater && floatersunderWater==0)
        {
            underwater = false;
            SwitchState(false);
        }
        // if (!underwater && waterExitTime + 1f < Time.time)
        // {
        //     if(RB.velocity.y > 0)
        //     RB.velocity = new Vector3(RB.velocity.x, RB.velocity.y * 0.99f, RB.velocity.z);
        // }
    }
    void SwitchState(bool isunderwater)
    {
        RB.velocity = new Vector3(RB.velocity.x, RB.velocity.y * waterSurfaceTension, RB.velocity.z);
        if (isunderwater)
        {
            RB.drag = underWaterDrag;
            RB.angularDrag = underWaterAngularDrag;
        }
        else
        {
            RB.drag = airDrag;
            RB.angularDrag = airAngularDrag;
            waterHeight = 0;
            // waterExitTime = Time.time;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            waterHeight = collision.gameObject.transform.position.y + collision.gameObject.transform.localScale.y / 2;
        }
    }

    // private void OnTriggerExit(Collider collision)
    // {
    //     if (collision.gameObject.CompareTag("Water"))
    //     {
    //         waterHeight = 0;
    //     }
    // }
}
