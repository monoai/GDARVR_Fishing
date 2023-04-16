using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rod : MonoBehaviour
{
    public GameObject markerRef;
    public GameObject marker;
    public bool isMarkerNull = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray r = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        Debug.DrawRay(r.origin, r.direction * 10f, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit, Mathf.Infinity))//, layerMask))
        {
            if (hit.collider.tag == "pond")
            {
                Debug.Log("hit: " + hit.collider.name);
                if (isMarkerNull)
                {
                    marker = Instantiate(markerRef, hit.point, Quaternion.identity);
                    isMarkerNull = false;
                }
                else
                {
                    marker.transform.position = hit.point;
                }
            }
        }
    }
}
