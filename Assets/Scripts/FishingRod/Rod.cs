using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rod : MonoBehaviour
{
    public GameObject marker;
    public GameObject baitRef;
    public GameObject bait;

    public MeshRenderer meshRenderer;
    public bool isMarkerNull = true;
    public bool isMarkerSet = false;
    public bool isBaitCast = false;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = marker.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        SetMarkerLocation();
    }

    public void CastBait()
    {
        if (bait != null)
        {
            Destroy(bait);
            marker.SetActive(true);
            isBaitCast = false;
            isMarkerSet = false;
        }
        else if (isMarkerSet && !isBaitCast)
        {
            bait = Instantiate(baitRef, marker.transform.position, Quaternion.identity);
            marker.SetActive(false);
            isBaitCast = true;
        }
        
    }

    public void SetMarker()
    {
        if (!isMarkerNull && !isBaitCast)
        {
            isMarkerSet = !isMarkerSet;
        }
        else
        {
            Debug.Log("Look for a pond first!!");
        }
    }

    private void SetMarkerLocation()
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
                    marker = Instantiate(marker, hit.point, Quaternion.identity);
                    isMarkerNull = false;
                }
                else if (!isMarkerSet)
                {
                    Color redColor = new Color(1.0f, 0.0f, 0.0f, 0.5f);
                    meshRenderer.sharedMaterial.color = redColor;
                    marker.transform.position = hit.point;
                }
                else
                {
                    Color greenColor = new Color(0.0f, 1.0f, 0.0f, 0.5f);
                    meshRenderer.sharedMaterial.color = greenColor;
                }
            }
        }
    }
}
