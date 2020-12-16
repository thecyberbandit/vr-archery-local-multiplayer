using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FruitController : MonoBehaviour
{
    public static FruitController instance;

    public FruitTransformController fruitTransformController;

    public GameObject[] mangoes;
    public GameObject[] grapes;


    private void Awake()
    {
        instance = this;
    }


    public void CreateFruit()
    {
        CreateYMango();
        CreateGMango();
        CreateBMango();
        CreateGGrape();
        CreateBGGrape();
    }

    public void CreateYMango()
    {
        int length = fruitTransformController.YellowMangoPositions.Count;

        for (int i = 0; i < length; i++)
        {
            if (fruitTransformController.YellowMangoPositions[i].GetComponent<Point>().canPlace)
            {
                //indexPosition = i;
                fruitTransformController.YellowMangoPositions[i].GetComponent<Point>().canPlace = false;
                GameObject yMango = Instantiate(mangoes[0]);
                yMango.GetComponent<Fruit>().point = i;
                yMango.transform.parent = fruitTransformController.YellowMangoPositions[i].parent;
                yMango.transform.position = fruitTransformController.YellowMangoPositions[i].position;
                yMango.transform.rotation = fruitTransformController.YellowMangoPositions[i].rotation;
                yMango.transform.localScale = fruitTransformController.YellowMangoPositions[i].localScale;
                yMango.GetComponent<Fruit>().mySpawnPoint = fruitTransformController.YellowMangoPositions[i].GetComponent<Point>();
                NetworkServer.Spawn(yMango);
                //yMango.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
            }
        }
    }

    public void CreateGMango()
    {
        int length = fruitTransformController.GreenMangoPositions.Count;

        for (int i = 0; i < length; i++)
        {
            if (fruitTransformController.GreenMangoPositions[i].GetComponent<Point>().canPlace)
            {
                //indexPosition = i;
                fruitTransformController.GreenMangoPositions[i].GetComponent<Point>().canPlace = false;
                GameObject gMango = Instantiate(mangoes[1]);
                gMango.GetComponent<Fruit>().point = i;
                gMango.transform.parent = fruitTransformController.GreenMangoPositions[i].parent;
                gMango.transform.position = fruitTransformController.GreenMangoPositions[i].position;
                gMango.transform.rotation = fruitTransformController.GreenMangoPositions[i].rotation;
                gMango.transform.localScale = fruitTransformController.GreenMangoPositions[i].localScale;
                NetworkServer.Spawn(gMango);
                //yMango.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
            }
        }
    }

    public void CreateBMango()
    {
        int length = fruitTransformController.BadmangoPositions.Count;

        for (int i = 0; i < length; i++)
        {
            if (fruitTransformController.BadmangoPositions[i].GetComponent<Point>().canPlace)
            {
                //indexPosition = i;
                fruitTransformController.BadmangoPositions[i].GetComponent<Point>().canPlace = false;
                GameObject bMango = Instantiate(mangoes[2]);
                bMango.GetComponent<Fruit>().point = i;
                bMango.transform.parent = fruitTransformController.BadmangoPositions[i].parent;
                bMango.transform.position = fruitTransformController.BadmangoPositions[i].position;
                bMango.transform.rotation = fruitTransformController.BadmangoPositions[i].rotation;
                bMango.transform.localScale = fruitTransformController.BadmangoPositions[i].localScale;
                NetworkServer.Spawn(bMango);
                //yMango.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
            }
        }
    }

    public void CreateGGrape()
    {
        int length = fruitTransformController.goodGrapePositions.Count;

        for (int i = 0; i < length; i++)
        {
            if (fruitTransformController.goodGrapePositions[i].GetComponent<Point>().canPlace)
            {
                //indexPosition = i;
                fruitTransformController.goodGrapePositions[i].GetComponent<Point>().canPlace = false;
                GameObject gGrape = Instantiate(grapes[0]);
                gGrape.GetComponent<Fruit>().point = i;
                gGrape.transform.parent = fruitTransformController.goodGrapePositions[i].parent;
                gGrape.transform.position = fruitTransformController.goodGrapePositions[i].position;
                gGrape.transform.rotation = fruitTransformController.goodGrapePositions[i].rotation;
                gGrape.transform.localScale = fruitTransformController.goodGrapePositions[i].localScale;
                NetworkServer.Spawn(gGrape);
                //yMango.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
            }
        }
    }

    public void CreateBGGrape()
    {
        int length = fruitTransformController.BadGrapePositions.Count;

        for (int i = 0; i < length; i++)
        {
            if (fruitTransformController.BadGrapePositions[i].GetComponent<Point>().canPlace)
            {
                //indexPosition = i;
                fruitTransformController.BadGrapePositions[i].GetComponent<Point>().canPlace = false;
                GameObject bGrape = Instantiate(grapes[1]);
                bGrape.GetComponent<Fruit>().point = i;
                bGrape.transform.parent = fruitTransformController.BadGrapePositions[i].parent;
                bGrape.transform.position = fruitTransformController.BadGrapePositions[i].position;
                bGrape.transform.rotation = fruitTransformController.BadGrapePositions[i].rotation;
                bGrape.transform.localScale = fruitTransformController.BadGrapePositions[i].localScale;
                NetworkServer.Spawn(bGrape);
                //yMango.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
            }
        }
    }

    public void  GenerateFruit_InPoint(string tag, int index)
    {
        StartCoroutine(GenerateFruitAfterDelay(tag, index));
    }

    IEnumerator GenerateFruitAfterDelay(string tag, int index)
    {
        yield return new WaitForSeconds(10f);

        GameObject go;
        switch (tag)
        {
            case "gy":
                go = Instantiate(mangoes[0], fruitTransformController.YellowMangoPositions[index].position,
                    fruitTransformController.YellowMangoPositions[index].rotation, fruitTransformController.YellowMangoPositions[index].transform.parent);
                go.transform.localScale = fruitTransformController.YellowMangoPositions[index].localScale;
                go.GetComponent<Fruit>().point = index;
                NetworkServer.Spawn(go);
                break;
            case "gg":
                go = Instantiate(mangoes[1], fruitTransformController.GreenMangoPositions[index].position,
                    fruitTransformController.GreenMangoPositions[index].rotation, fruitTransformController.GreenMangoPositions[index].transform.parent);
                go.transform.localScale = fruitTransformController.GreenMangoPositions[index].localScale;
                go.GetComponent<Fruit>().point = index;
                NetworkServer.Spawn(go);
                break;
            case "by":
                go = Instantiate(mangoes[2], fruitTransformController.BadmangoPositions[index].position,
                    fruitTransformController.BadmangoPositions[index].rotation, fruitTransformController.BadmangoPositions[index].transform.parent);
                go.transform.localScale = fruitTransformController.BadmangoPositions[index].localScale;
                go.GetComponent<Fruit>().point = index;
                NetworkServer.Spawn(go);
                break;
            case "gp":
                go = Instantiate(grapes[0], fruitTransformController.goodGrapePositions[index].position,
                    fruitTransformController.goodGrapePositions[index].rotation, fruitTransformController.goodGrapePositions[index].transform.parent);
                go.transform.localScale = fruitTransformController.goodGrapePositions[index].localScale;
                go.GetComponent<Fruit>().point = index;
                NetworkServer.Spawn(go);
                break;
            case "bp":
                go = Instantiate(grapes[1], fruitTransformController.BadGrapePositions[index].position,
                    fruitTransformController.BadGrapePositions[index].rotation, fruitTransformController.BadGrapePositions[index].transform.parent);
                go.transform.localScale = fruitTransformController.BadGrapePositions[index].localScale;
                go.GetComponent<Fruit>().point = index;
                NetworkServer.Spawn(go);
                break;
            default:
                break;
        }
    }
}
