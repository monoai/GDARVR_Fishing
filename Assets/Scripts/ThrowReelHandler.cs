using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowReelHandler : MonoBehaviour
{
    public float distanceThreshold = 1f;
    private Vector3 prevAccel = Vector3.zero;

    public bool isThrown = false;
    public bool isReeled = false; 

    // Update is called once per frame
    void Update()
    {
        if (prevAccel == Vector3.zero)
        {
            prevAccel = Input.acceleration;
            return;
        }

        float distance = Vector3.Distance(prevAccel, Input.acceleration);

        if (Mathf.Abs(distance) >= distanceThreshold)
        {
            if (!isThrown)
            {
                isThrown = true;
                isReeled = false;
            }
            else
            {
                isThrown = false;
                isReeled = true;
            }
        }

        prevAccel = Input.acceleration;
    }
}