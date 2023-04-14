using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    // Enums
    private enum FishType
    {
        Normal,
        Tuna,
        Gold
    };

    // Models
    [Header("Fish Models")]
    [SerializeField] private GameObject normalModel;
    [SerializeField] private GameObject tunaModel;
    [SerializeField] private GameObject goldModel;
    [SerializeField] private GameObject tempModel;

    [Header("Fish Data")]
    [SerializeField] private FishType currType;
    [SerializeField] private bool isCaught = false;
    [SerializeField] private float interest = 0.0f;

    [Header("References/Sensors")]
    // NOTE: Temporarily, fishes are directly assigned with the bait reference
    // Ideally fish spawning on start should try to find the bait so they could get a reference
    // IDEA: If we're essentially finding the bait reference in the first place
    // then why not instantiate new bait?
    // COUNTER-POINT: New bait means existing fish loses reference, and instantiating is costly
    // It seems wiser to let new fish find the reference rather than ALL fishes losing all references.
    [SerializeField] private GameObject bait;


    // Start is called before the first frame update
    void Start()
    {
        // We assume that the type and data is only initialized once
        GameObject model = normalModel;
        switch (currType) {
            case FishType.Tuna:
                model = tunaModel;
                break;
            case FishType.Gold:
                model = goldModel;
                break;
            default:
                model = normalModel;
                break;
        }
        model = Instantiate(model, this.transform);
        model.name = "Model";
        Destroy(tempModel);
    }

    // Update is called once per frame
    void Update()
    {
        var baitstate = bait.GetComponent<DummyBait>();

        // Being caught is highest priority
        if (baitstate.currState == DummyBait.BaitState.FishCaught && this.isCaught == true)
        {
            Debug.Log(this.name + "is caught");
        }
        else
        {
            // If the bait is cast, no one is caught, and interest is < 100 then second priority
            if (baitstate.currState == DummyBait.BaitState.Cast && baitstate.currState != DummyBait.BaitState.FishCaught && this.interest >= 100.0f)
            {
                Debug.Log(this.name + "is looking for the bait");
            }
            // If the bait is cast, no one is caught, and interest is >= 100
            else if (baitstate.currState == DummyBait.BaitState.Cast && baitstate.currState != DummyBait.BaitState.FishCaught && this.interest < 100.0f)
            {
                Debug.Log(this.name + "is gaining interest");
            }
            // else, assume default behavior of nothing and then swim around
            else
            {
                Debug.Log(this.name + "is just hanging around");
            }
        }
    }
}
