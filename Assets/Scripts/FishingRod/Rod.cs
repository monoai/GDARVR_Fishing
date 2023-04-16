using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rod : MonoBehaviour
{
    [SerializeField] private ThrowReelHandler rodHandler;
    [SerializeField] private Bait bait;
    public GameObject marker;

    public bool isMarkerNull = true;
    public bool isMarkerSet = false;
    bool captureFish = false;

    private float caught_ticks = 0;
    private float reel_ticks = 0;

    private float caught_INTERVAL = 5.0f;
    private float reel_INTERVAL = 3.0f;

    MeshRenderer markerMeshRenderer;
    MeshRenderer baitMeshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        markerMeshRenderer = marker.GetComponent<MeshRenderer>();
        baitMeshRenderer = bait.GetComponent<MeshRenderer>();
        baitMeshRenderer.sharedMaterial.color = Color.white;
        bait.currState = Bait.BaitState.Released;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMarkerLocation();
        ConfirmMarkerLocation();
        //CastBaitGesture();

        if (bait.currState == Bait.BaitState.FishCaught)
        {
            caught_ticks += Time.deltaTime;
            if (caught_ticks > caught_INTERVAL)
            {
                caught_ticks = 0;
                bait.caughtFish.gotReleased();
                baitMeshRenderer.sharedMaterial.color = Color.white;
                bait.currState = Bait.BaitState.Cast;
                // Lost Fish!!
            }

            if (Input.GetMouseButton(1))
            {
                reel_ticks += Time.deltaTime;
                baitMeshRenderer.sharedMaterial.color = new Color(1.0f, 0.5f, 0.0f, 1.0f);
                if (captureFish)
                {
                    baitMeshRenderer.sharedMaterial.color = Color.white;
                    bait.currState = Bait.BaitState.Released;
                    captureFish = false;
                    isMarkerSet = false;
                    marker.SetActive(true);
                    bait.gameObject.SetActive(false);
                    // Captured Fish!!
                }
                else if (reel_INTERVAL > caught_INTERVAL)
                {
                    reel_ticks = 0;
                    bait.caughtFish.gotReleased();
                    baitMeshRenderer.sharedMaterial.color = Color.white;
                    bait.currState = Bait.BaitState.Cast;
                    // Lost Fish!!
                }
            }
            else
            {
                baitMeshRenderer.sharedMaterial.color = Color.white;
            }
        }

        //if (bait.currState == Bait.BaitState.Succeed)
        //{
        //    bait.currState = Bait.BaitState.Released;
        //    captureFish = false;
        //    isMarkerSet = false;
        //    marker.SetActive(true);
        //    bait.gameObject.SetActive(false);
        //}
    }

    public void CastBaitGesture()
    {
        if (bait.currState == Bait.BaitState.Cast)
        {
            bait.currState = Bait.BaitState.Released;
            isMarkerSet = false;
            marker.SetActive(true);
            bait.gameObject.SetActive(false);
        }
        else if (isMarkerSet && bait.currState == Bait.BaitState.Released && rodHandler.isThrown)
        {
            bait.currState = Bait.BaitState.Cast;
            bait.gameObject.transform.position = marker.transform.position;
            bait.gameObject.SetActive(true);
            marker.SetActive(false);
        }
    }

    public void CastBaitButton()
    {
        if (bait.currState == Bait.BaitState.Cast)
        {
            bait.currState = Bait.BaitState.Released;
            marker.SetActive(true);
            bait.gameObject.SetActive(false);
        }
        else if (isMarkerSet && bait.currState == Bait.BaitState.Released)
        {
            Debug.Log("CASTING BAIT!!!");
            bait.currState = Bait.BaitState.Cast;
            bait.gameObject.transform.position = marker.transform.position;
            bait.gameObject.SetActive(true);
            marker.SetActive(false);
        }
        else if (bait.currState == Bait.BaitState.FishCaught)
        {
            captureFish = true;
        }
    }

    public void ConfirmMarkerLocation()
    {
        if (Input.touchCount > 0)
        {
            Debug.Log("Screen touched.");
            if (!isMarkerNull && bait.currState == Bait.BaitState.Released)
            {
                isMarkerSet = true;
            }
        }
        else if (Input.GetMouseButton(1))
        {
            Debug.Log("RMB pressed.");
            if (!isMarkerNull && bait.currState == Bait.BaitState.Released)
            {
                isMarkerSet = true;
            }
        }
        else
        {
            isMarkerSet = false;
        }
    }

    public void SetMarkerButton()
    {
        if (!isMarkerNull && bait.currState == Bait.BaitState.Released)
        {
            isMarkerSet = !isMarkerSet;
        }
        else
        {
            Debug.Log("Look for a pond first!!");
        }
    }

    private void UpdateMarkerLocation()
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
                    markerMeshRenderer.sharedMaterial.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
                    marker.transform.position = hit.point;
                }
                else
                {
                    markerMeshRenderer.sharedMaterial.color = new Color(0.0f, 1.0f, 0.0f, 0.5f);
                }
            }
        }
    }

    public void CatchFish()
    {
        bait.currState = Bait.BaitState.FishCaught;
    }
}
