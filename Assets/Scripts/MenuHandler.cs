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
    bool inPond = false;

    void Start()
    {
        menu.SetActive(true);
        pondUI.SetActive(false);
        album.SetActive(false);
    }

    public void OnPlay()
    {
        menu.SetActive(false);
        scanMessage.SetActive(true);
        inPond = true;
    }

    public void OnAlbum()
    {
        menu.SetActive(false);
        album.SetActive(true);
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
        }
    }

    public void OnPondMarkerLost()
    {
        scanMessage.SetActive(true);
        pondUI.SetActive(false);
    }
}
