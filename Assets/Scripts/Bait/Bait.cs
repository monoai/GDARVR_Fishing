using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

/* look at me
 * im the actual bait script now
 */

public class Bait : MonoBehaviour
{
    public Fish caughtFish;
    
    // Enums
    // NOTE: These states may be necessary in order to access the bait data easily
    // Idea: Setters-getters in order to preserve OOP? I don't got time to think about that.
    public enum BaitState
    {
        Released, // Not yet cast, should not be on the pond
        Cast, // Casted, should be on the pond
        FishCaught, // Caught a fish, should be on the pond
        Succeed
    };

    [SerializeField] public BaitState currState;
    // Start is called before the first frame update
    void Start()
    {
        this.currState = BaitState.Released;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider obj)
    {
        // yes this works even through all my test cases
        if(obj.CompareTag("Fish") && obj.GetComponent<Fish>().isScared != true && this.currState == BaitState.Cast) {
            obj.GetComponent<Fish>().gotCaught();
            this.currState = BaitState.FishCaught;
            caughtFish = obj.GetComponent<Fish>();
        }
    }
}
