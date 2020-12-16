using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayButtonController : NetworkBehaviour
{
    public static PlayButtonController instance;

    public NetworkManager manager;
    public GameObject playButton1;
    public GameObject playButton2;
    public GameObject uiCamera;

    private bool button1Clicked = false;
    private bool button2Clicked = false;


    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        CheckForPlayers();
    }

    [Server]
    void CheckForPlayers()
    {
        if (manager.numPlayers <2)
        {
            return;
        }

        else if (manager.numPlayers == 2)
        {
            //manager.ServerChangeScene("archeryScene");
            playButton1.SetActive(true);
            playButton2.SetActive(true);
            uiCamera.SetActive(false);
        }
    }
}
