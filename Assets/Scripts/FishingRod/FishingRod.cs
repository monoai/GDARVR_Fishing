using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class FishingRod : MonoBehaviour
{
    public ContentPositioningBehaviour reference;

    public GameObject setMarkerButton;
    public GameObject castButton;
    public GameObject reelButton;
    public bool isLocationSet = false;
    public bool isCast = false;

    public void OnInteractiveHitTest(HitTestResult result)
    {
        Ray r = Camera.main.ScreenPointToRay(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        Debug.DrawRay(r.origin, r.direction * 10f, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit, Mathf.Infinity))//, layerMask))
        {
            if (hit.collider.tag == "pond")
            {
                Debug.Log("hit: " + hit.collider.name);
            }
        }
    }

    public void OnAutomaticHitTest(HitTestResult result)
    {
        if (reference != null)
        {
            if (isLocationSet) // marker location has been set
            {
                if (isCast) // confirmation to cast bait at marker location
                {
                    CastBait();
                    setMarkerButton.SetActive(false);
                    reelButton.SetActive(true);
                }
                else // cancel fishing/pull back bait
                {
                    setMarkerButton.SetActive(true);
                    reelButton.SetActive(false);
                }
            }
            else
            {
                reference.PositionContentAtPlaneAnchor(result);
            }
        }
    }


    public void CastBait()
    {

    }

    public void SetMarkerLocation()
    {
        isLocationSet = !isLocationSet;
    }

    public void SetCast()
    {
        isCast = !isCast;
    }
}
