using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rod : MonoBehaviour
{
    [SerializeField] private ThrowReelHandler rodHandler;
    [SerializeField] private GameObject baitRef;
    public GameObject bait;
    public GameObject marker;

    public bool isMarkerNull = true;
    public bool isMarkerSet = false;
    public bool isBaitCast = false;

    MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = marker.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        SetMarkerLocation();
        SetMarkerGesture();
        //CastBaitGesture();
    }

    public void CastBaitGesture()
    {
        if (bait != null && rodHandler.isReeled)
        {
            Destroy(bait);
            marker.SetActive(true);
            isBaitCast = false;
            isMarkerSet = false;
        }
        else if (isMarkerSet && !isBaitCast && rodHandler.isThrown)
        {
            bait = Instantiate(baitRef, marker.transform.position, Quaternion.identity);
            marker.SetActive(false);
            isBaitCast = true;
        }
    }

    public void CastBaitButton()
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

    public void SetMarkerGesture()
    {
        if (Input.touchCount > 0)
        {
            Debug.Log("Screen touched.");
            if (!isMarkerNull && !isBaitCast)
            {
                isMarkerSet = true;
            }
        }
        else if (Input.GetMouseButton(1))
        {
            Debug.Log("RMB pressed.");
            if (!isMarkerNull && !isBaitCast)
            {
                isMarkerSet = true;
            }
        }
        else
        {
            isMarkerSet = false;
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
