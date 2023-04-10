using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject pond;
    [SerializeField] private GameObject album;

    void Start()
    {
        menu.SetActive(true);
        pond.SetActive(false);
        album.SetActive(false);
    }

    public void OnPlay()
    {
        menu.SetActive(false);
        pond.SetActive(true);
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
        pond.SetActive(false);
        menu.SetActive(true);
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
