using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

/* This script's purpose is to provide a temporary bait behavior so I could
 * program the fish and someone can just swap the code later on.
 */

public class DummyBait : MonoBehaviour
{
    // Enums
    // NOTE: These states may be necessary in order to access the bait data easily
    // Idea: Setters-getters in order to preserve OOP? I don't got time to think about that.
    public enum BaitState
    {
        Released,
        Cast,
        FishCaught
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
}
