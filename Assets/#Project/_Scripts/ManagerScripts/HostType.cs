using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostType : MonoBehaviour
{
    public static HostType hostType;

    private void Awake()
    {
        if(hostType==null)
        {
            hostType = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public enum PlayerType
    {
        none, host, client
    }

    public PlayerType type;
}
