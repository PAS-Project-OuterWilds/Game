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

    //debug
    private bool touchedWater = false;

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
    }
    void SwitchState(bool isunderwater)
    {
        if (isunderwater)
        {
            RB.drag = underWaterDrag;
            RB.angularDrag = underWaterAngularDrag;
        }
        else
        {
            RB.drag = airDrag;
            RB.angularDrag = airAngularDrag;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            touchedWater = true;
            waterHeight = collision.gameObject.transform.position.y;
        }
    }
}
