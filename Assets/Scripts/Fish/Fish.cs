using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

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
    [SerializeField] private float speed = 1.0f;

    [Header("Fish Information")]
    [SerializeField] private int fishValue = 0;

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
        // Initialize default values
        interest = Random.Range(0.0f, 100.0f);

        // We assume that the type and data is only initialized once
        // Any values that should be overriden based on type will be done here
        GameObject model = normalModel;
        switch (currType) {
            case FishType.Tuna:
                model = tunaModel;
                fishValue = 5;
                break;
            case FishType.Gold:
                model = goldModel;
                fishValue = 25;
                break;
            default:
                model = normalModel;
                fishValue = 1;
                break;
        }
        model = Instantiate(model, this.transform);
        model.name = "Model";
        Destroy(tempModel);
    }

    // Update is called once per frame
    void Update()
    {
        var baitstate = bait.GetComponent<Bait>();

        // If caught, place the behavior here

        // Being caught is highest priority
        if (baitstate.currState == Bait.BaitState.FishCaught && this.isCaught == true)
        {
            Debug.Log(this.name + "is caught");
        }
        else
        {
            // If the bait is cast, no one is caught, and interest is < 100 then second priority
            if (baitstate.currState == Bait.BaitState.Cast && baitstate.currState != Bait.BaitState.FishCaught && this.interest >= 100.0f)
            {
                Debug.Log(this.name + "is looking for the bait");
                // Move towards the bait behavior
                Vector3 rotDir = Vector3.RotateTowards(transform.forward, bait.transform.position - transform.position, Time.deltaTime * this.speed * 2.5f, 0.0f);
                //Debug.DrawRay(transform.position, rotDir, Color.red);
                transform.position = Vector3.MoveTowards(transform.position, bait.transform.position, Time.deltaTime * this.speed);
                transform.rotation = Quaternion.LookRotation(rotDir);
            }
            // If the bait is cast, no one is caught, and interest is >= 100
            else if (baitstate.currState == Bait.BaitState.Cast && baitstate.currState != Bait.BaitState.FishCaught && this.interest < 100.0f)
            {
                Debug.Log(this.name + "is gaining interest");
                /* note: logic code done by shiro
                 * if(distance of transform.position and bait < 10)
                 *      interest += 1.0f;
                 * else if(distance of transform.position and bait < 50)
                 *      interest += 0.50f;
                 * else/else if(distance of transform.position and bait < 100)
                 *      interest += 0.25
                 * 
                 */
                interest += 1.0f;
            }
            // else, assume default behavior of nothing and then swim around
            else
            {
                Debug.Log(this.name + "is just hanging around");
                // Random move Behavior

                /* note to mono: make sure to check the limitations of unit circle
                 * note to shiro and kuro: check if you guys could refactor the movement of the fish so we could just reuse the code
                 * note etc: as you can see, we can use currDestination for the bait maaaaybe?
                 * if(distance of transform.position < currDestination)
                 *      changeDestination();
                 *      something else
                 * else
                 *      moveTowards currDestination
                 *      rotTowards currDestination
                 */
                //transform.Translate((Random.insideUnitCircle * this.radius) * Time.deltaTime);
            }
        }
    }

    public void gotCaught() {
        isCaught = true;
        interest = 0.0f;
    }
}
