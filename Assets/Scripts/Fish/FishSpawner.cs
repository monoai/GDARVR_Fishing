using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{

    public Transform WaterPlane;
    Vector3 PlaneSize;
    [SerializeField] private float SpawnTimer = 10.0f;
    [SerializeField] private Fish fishRef;
    [SerializeField] private GameObject baitRef;
    //public List<Fish> fishList;
    private bool canSpawn = true;

    private bool PoolFound = false;

    // Start is called before the first frame update
    void Start()
    {
        PlaneSize = WaterPlane.GetComponent<Renderer>().bounds.size; 
    }

    // Update is called once per frame
    void Update()
    {
        if(canSpawn && PoolFound){
            StartCoroutine(SpawnFish());
        }
    }

    IEnumerator SpawnFish(){
        Debug.Log("Spawning Fish");
        Fish temp = Instantiate(fishRef, WaterPlane);
        temp.bait = baitRef;
        temp.transform.localPosition = new Vector3(Random.Range(0, 5), 0.0f,Random.Range(-5.0f, 0));
        temp.gameObject.SetActive(true);
        canSpawn = false;
        yield return new WaitForSeconds(SpawnTimer);
        canSpawn = true;
    }

    public void PoolDetected(bool found){
        PoolFound = found;
    }
}
