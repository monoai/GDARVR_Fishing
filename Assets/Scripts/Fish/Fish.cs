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
        
    }
}
