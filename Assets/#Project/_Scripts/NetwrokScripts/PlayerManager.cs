using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class PlayerManager : NetworkBehaviour
{
    public float myYellow = 0;
    public float myGreen = 0;
    public float myPurple = 0;
    [SyncVar] public float opponentYellow = 0;
    [SyncVar] public float opponentGreen = 0;
    [SyncVar] public float opponentPurple = 0;

    bool isFilled = false;
    

    private Transform initialPos;

    int isHost;

    public Image yellowBar1, greenBar1, grapeBar1;

    private void Awake()
    {

        initialPos = this.transform;
    }

    private void Start()
    {
        if (!isLocalPlayer) return;

        if (HostType.hostType.type == HostType.PlayerType.host)
        {
            transform.tag = "player1";
            isHost = 0;
            Debug.Log("player1 tag is set" +transform.tag);
        }

        else
        {
            transform.tag = "player2";
            isHost = 1;
            Debug.Log("player2 tag is set" + transform.tag);
        }

        yellowBar1 = ViveManager.instance.yellowBar;
        greenBar1 = ViveManager.instance.greenBar;
        grapeBar1 = ViveManager.instance.grapeBar;
    }


    void Update()
    {
        if (!isLocalPlayer) return;

        yellowBar1.fillAmount = myYellow;
        greenBar1.fillAmount = myGreen;
        grapeBar1.fillAmount = myPurple;

        if (GameManager.Instance.isGameOver)
        {
            GameOver();
        }
    }

    public void ServerReady()
    {
        CmdServerReady(true);
    }

    public void ClientReady()
    {
        CmdClientReady(true);
    }

    [Command]
    void CmdServerReady(bool server)
    {
        RpcServerReady(server);
    }

    [Command]
    void CmdClientReady(bool client)
    {
        RpcClientReady(client);
    }

    [ClientRpc]
    void RpcServerReady(bool server)
    {
        GameManager.Instance.serverReady = server;
    }

    [ClientRpc]
    void RpcClientReady(bool client)
    {
        GameManager.Instance.clientReady = client;
    }

    public void DestroyFruit(uint id)
    {
        CmdDestroyFruit(id);
    }

    [Command]
    void CmdDestroyFruit(uint id)
    {
        GameObject go = NetworkIdentity.spawned[id].gameObject;
        NetworkServer.Destroy(go);
    }

    public void IncreaseScore(string tag)
    {
        if (tag == "gy")
        {
            myYellow += 0.1f;
        }

        else if (tag == "gg")
        {
            myGreen += 0.1f;
        }

        else if (tag == "gp")
        {
            myPurple += 0.1f;
        }

        Debug.Log("1 increased");
    }

    public void DecreaseScore(string tag)
    {
        if (tag == "by")
        {
            myYellow -= 0.1f;
        }

        else if (tag == "by")
        {
            myGreen -= 0.1f;
        }

        else if (tag == "bp")
        {
            myPurple -= 0.1f;
        }

        Debug.Log("1 decreased");
    }

    void GameOver()
    {
        if (yellowBar1.fillAmount == 1 && greenBar1.fillAmount == 1 && grapeBar1.fillAmount == 1)
        {
            GameManager.Instance.winImage.SetActive(true);

            int host;

            if (HostType.hostType.type == HostType.PlayerType.host)
            {
                host = 0;
            }

            else
            {
                host = 1;
            }

            CmdGameOver(host);
        }

        else
        {
            if (isFilled)
            {
                GameManager.Instance.looseImage.SetActive(true);
            }

            else
            {
                GameManager.Instance.drawImage.SetActive(true);
            }
        }
    }

    [Command]
    void CmdGameOver(int host)
    {
        RpcGameOver(host);
    }

    [ClientRpc]
    void RpcGameOver(int host)
    {
        if (HostType.hostType.type == HostType.PlayerType.host)
        {
            if (host == 0)
            {
                return;
            }

            else
            {
                isFilled = true;
            }
        }

        else if (HostType.hostType.type == HostType.PlayerType.client)
        {
            if (host == 1)
            {
                return;
            }

            else
            {
                isFilled = true;
            }
        }
    }
}
