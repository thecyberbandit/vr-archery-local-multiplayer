using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public static ParticleController instance;


    public GameObject yellow;
    public GameObject green;
    public GameObject grape;


    public void PlayParticle(Transform trans, string tag)
    {
        if (tag == "gy" || tag == "by")
        {
            yellow.transform.SetParent(trans.parent);
            yellow.transform.position = trans.transform.position;
            yellow.GetComponent<ParticleSystem>().Play();
        }

        else if (tag == "gg" || tag == "bg")
        {
            green.transform.SetParent(trans.parent);
            green.transform.position = trans.transform.position;
            green.GetComponent<ParticleSystem>().Play();
        }

        else
        {
            grape.transform.SetParent(trans.parent);
            grape.transform.position = trans.transform.position;
            grape.GetComponent<ParticleSystem>().Play();
        }
    }
}
