using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowReelHandler : MonoBehaviour
{
    public float distanceThreshold = 2f;
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

        if (prevAccel.sqrMagnitude >= distanceThreshold)
        {
            isReeled = true;
        }

        prevAccel = Input.acceleration;
    }
}
