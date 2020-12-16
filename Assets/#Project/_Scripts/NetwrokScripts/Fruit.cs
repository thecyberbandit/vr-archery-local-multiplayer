using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Fruit : NetworkBehaviour
{
    [HideInInspector]
    public string myTag;
    public int point;
    public Point mySpawnPoint;

    bool isGood;

    Transform initialPos;

    public ParticleSystem myParticle;

    public NetworkIdentity network;

    uint id;


    private void Start()
    {
        myTag = this.gameObject.tag;
        initialPos = this.transform;

        if (myTag == "gy" || myTag == "gg" || myTag == "gp")
        {
            isGood = true;
        }

        else
        {
            isGood = false;
        }

        myParticle = GetComponentInChildren<ParticleSystem>();
        network = GetComponent<NetworkIdentity>();
        
        id = network.netId;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("D pressed");
            //DestroySelf();
            //CmdDestroy(id);
        }
    }

    //[Server]
    //void DestroySelf()
    //{
    //    NetworkServer.Destroy(this.gameObject);
    //}

    //[Command]
    //void CmdDestroy(uint id)
    //{
    //    //RpcDestroy(id);
    //    NetworkServer.Destroy(this.gameObject);
    //}

    //[ClientRpc]
    //void RpcDestroy(uint id)
    //{
    //    GameObject go = NetworkIdentity.spawned[id].gameObject;
    //    go.SetActive(false);
    //}

    //[ServerCallback]
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("p1"))
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;


            if (myParticle.isPlaying)
            {
                myParticle.Stop();
                myParticle.Play();
            }

            else
            {
                myParticle.Play();
            }

            if (isGood)
            {
                GameObject.FindGameObjectWithTag("player1").GetComponent<PlayerManager>().IncreaseScore(myTag);
                AudioManager.instance.PlaySound("pop");
                //FruitController.instance.CreateFruit(myTag, initialPos);
            }

            else
            {
                GameObject.FindGameObjectWithTag("player1").GetComponent<PlayerManager>().DecreaseScore(myTag);
                AudioManager.instance.PlaySound("wrong");
            }
            FruitController.instance.GenerateFruit_InPoint(myTag, point);

            StartCoroutine(WaitForParticle("player1", id));
        }

        else if (other.CompareTag("p2"))
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;

            if (myParticle.isPlaying)
            {
                myParticle.Stop();
                myParticle.Play();
            }

            else
            {
                myParticle.Play();
            }

            if (isGood)
            {
                GameObject.FindGameObjectWithTag("player2").GetComponent<PlayerManager>().IncreaseScore(myTag);
                AudioManager.instance.PlaySound("pop");
                //FruitController.instance.CreateFruit(myTag, initialPos);
            }

            else
            {
                GameObject.FindGameObjectWithTag("player2").GetComponent<PlayerManager>().DecreaseScore(myTag);
                AudioManager.instance.PlaySound("wrong");
                //FruitController.instance.CreateFruit(myTag, initialPos);
            }
            FruitController.instance.GenerateFruit_InPoint(myTag, point);
            
            StartCoroutine(WaitForParticle("player2", id));

            
        }

        //AudioManager.instance.PlaySound("pop");
        ////this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        ////this.gameObject.GetComponent<BoxCollider>().enabled = false;

        
        Debug.Log("Object Tag: " + other.gameObject.tag);

        //mySpawnPoint.canPlace = true;
        //FruitController.instance.CreateFruit();

        //NetworkServer.Destroy(this.gameObject);
        //if(GameManager.Instance.type==GameManager.PlayerType.host)
        //{
        //    NetworkServer.Destroy(this.gameObject);
        //}
        //else
        //{
        //    Debug.Log("Fruit id: " + id);
        //    FruitController.instance.DestroyFruit(id);
        //}
    }

    IEnumerator WaitForParticle(string name, uint id)
    {
        yield return new WaitForSeconds(2f);
        GameObject.FindGameObjectWithTag(name).GetComponent<PlayerManager>().DestroyFruit(id);
    }
}
