using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViveManager : MonoBehaviour
{
    public Text player1ScoreText;
    public Text player2ScoreText;

    public Image yellowBar;
    public Image greenBar;
    public Image grapeBar;

    public Text timerText;
    public RadialProgressBar bar;


    public static ViveManager instance;



    private void Awake()
    {
        instance = this;
    }
}
