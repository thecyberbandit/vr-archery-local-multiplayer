using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class AdminPanelController : MonoBehaviour
{
    public GameObject adminPanel;
    public InputField hostIP;
    public Text inputText;
    public Text serverReadyText;
    public Text clientReadyText;
    public Text addressText;

    public GameObject mainCamera;

    private bool isOpen = false;
    private bool serverReady = false, clientReady = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isOpen)
            {
                adminPanel.SetActive(true);
                isOpen = true;
                mainCamera.SetActive(true);
            }

            else
            {
                adminPanel.SetActive(false);
                mainCamera.SetActive(false);
                isOpen = false;
            }
        }

        if (NetworkServer.active)
        {
            serverReadyText.text = "Server is ready";
            addressText.text = NetworkManager.singleton.networkAddress;
            serverReady = true;
        }

        else if (NetworkClient.isConnected)
        {
            clientReadyText.text = "Client is ready";
            clientReady = true;
        }

        if (NetworkServer.active) //&& NetworkClient.isConnected)
        {
            //mainCamera.SetActive(false);
            //GameObject.FindGameObjectWithTag("MainCamera").SetActive(false);
        }
    }

    public void OnLANHostClicked()
    {
        NetworkManager.singleton.StartHost();
        HostType.hostType.type = HostType.PlayerType.host;
        //NetworkManager.singleton.players[0].tag = "player1";
        //StartCoroutine(CreateFruit());
        FruitController.instance.CreateYMango();
        FruitController.instance.CreateGMango();
        FruitController.instance.CreateBMango();
        FruitController.instance.CreateGGrape();
        FruitController.instance.CreateBGGrape();

        //GameManager.Instance.isGameStarted = true;
    }

    public void OnLANClientClicked()
    {
        NetworkManager.singleton.StartClient();
        HostType.hostType.type = HostType.PlayerType.client;
        //NetworkManager.singleton.players[1].tag = "player2";
    }

    public void GetHostIP()
    {
        NetworkManager.singleton.networkAddress = inputText.text;

        Debug.Log(NetworkManager.singleton.networkAddress);
    }

    IEnumerator CreateFruit()
    {
        yield return new WaitForSeconds(5f);
        FruitController.instance.CreateYMango();
    }
}
