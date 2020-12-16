using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitTransformController : MonoBehaviour
{
    public Transform goodGrapeParent;
    public Transform badGrapeParent;
    public Transform YellowMangoParent;
    public Transform GreenMangoparent;
    public Transform BadmangoParent;

    public int indexPosition;
    public Transform nextTransform;

    public List<Transform> goodGrapePositions;
    public List<Transform> BadGrapePositions;
    public List<Transform> YellowMangoPositions;
    public List<Transform> GreenMangoPositions;
    public List<Transform> BadmangoPositions;



    //private void Awake()
    //{
    //    goodGrapePositions = new List<Transform>();
    //    BadGrapePositions = new List<Transform>(); 
    //    YellowMangoPositions = new List<Transform>();
    //    GreenMangoPositions = new List<Transform>();
    //    BadmangoPositions = new List<Transform>();
    //}

    //public void Addpositions(Transform parent, List<Transform> T)
    //{
    //    foreach (Transform item in parent)
    //    {
    //        T.Add(item);
    //    }
    //}

    //private void Start()
    //{
    //    AddBGrapePos();
    //    AddBMangoPos();
    //    AddGGrapePos();
    //    AddGMangoPos();
    //    AddYmangoPos();
    //}

    //void AddGGrapePos()
    //{
    //    foreach (Transform item in goodGrapeParent)
    //    {
    //        goodGrapePositions.Add(item);
    //    }
    //}

    //void AddBGrapePos()
    //{
    //    foreach (Transform item in badGrapeParent)
    //    {
    //        BadGrapePositions.Add(item);
    //    }
    //}

    //void AddYmangoPos()
    //{
    //    foreach (Transform item in YellowMangoParent)
    //    {
    //        YellowMangoPositions.Add(item);
    //    }
    //}

    //void AddGMangoPos()
    //{
    //    foreach (Transform item in GreenMangoparent)
    //    {
    //        GreenMangoPositions.Add(item);
    //    }
    //}

    //void AddBMangoPos()
    //{
    //    foreach (Transform item in BadmangoParent)
    //    {
    //        BadmangoPositions.Add(item);
    //    }
    //}

    Transform GetBGrapepoint()
    {
        int length = BadGrapePositions.Count;

        for (int i = 0; i < length; i++)
        {
            if (BadGrapePositions[i].GetComponent<Point>().canPlace)
            {
                indexPosition = i;
                BadGrapePositions[i].GetComponent<Point>().canPlace = false;
                return BadGrapePositions[i];
            }

            else
            {
                BadGrapePositions[i].GetComponent<Point>().canPlace = false;
            }
        }

        indexPosition = 0;
        return null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            nextTransform = GetBGrapepoint();
        }
    }
}
