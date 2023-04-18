using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject pondUI;
    [SerializeField] private GameObject album;
    [SerializeField] private GameObject scanMessage;
    [SerializeField] private GameObject pondOne;
    [SerializeField] private GameObject fishingRod;
    [SerializeField] private GameObject ThrowReelHandler;

    [SerializeField] private GameObject normalFish;
    [SerializeField] private GameObject goldenFish;
    [SerializeField] private GameObject tunaFish;

    // FISH ALBUM VALUES //
    bool tunaCaught = false;
    bool goldenCaught = false;
    bool normalCaught = false;

    bool inPond = false;

    // SETTER FUNCTIONS //
    public void TunaIsCaught()
    {
        this.tunaCaught = true;
    }

    public void GoldenIsCaught()
    {
        this.goldenCaught = true;
    }

    public void NormalIsCaught()
    {
        this.normalCaught = true;
    }

    // INITIALIZE ON APP STARTUP//
    void Start()
    {
        menu.SetActive(true);
        pondUI.SetActive(false);
        album.SetActive(false);
        ThrowReelHandler.SetActive(false);
        scanMessage.SetActive(false);
        fishingRod.SetActive(false);
    }

    public void OnPlay()
    {
        menu.SetActive(false);
        scanMessage.SetActive(true);
        fishingRod.SetActive(true);
        inPond = true;
    }

    public void OnAlbum()
    {
        menu.SetActive(false);
        album.SetActive(true);

        normalFish.SetActive(normalCaught);
        tunaFish.SetActive(tunaCaught);
        goldenFish.SetActive(goldenCaught);
    }

    public void OnBackFromAlbum()
    {
        album.SetActive(false);
        menu.SetActive(true);
    }

    public void OnQuit()
    {
        pondUI.SetActive(false);
        pondOne.SetActive(false);
        scanMessage.SetActive(false);
        ThrowReelHandler.SetActive(false);

        menu.SetActive(true);
        inPond = false;
    }

    public void OnExit()
    {
        Application.Quit();
    }

    public void OnPondMarkerFound()
    {
        scanMessage.SetActive(false);

        if (inPond)
        {
            pondUI.SetActive(true);
            pondOne.SetActive(true);
            ThrowReelHandler.SetActive(true);
        }
    }

    public void OnPondMarkerLost()
    {
        scanMessage.SetActive(true);
        pondUI.SetActive(false);
        ThrowReelHandler.SetActive(false);
    }
}
