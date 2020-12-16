using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialProgressBar : MonoBehaviour {

    public Image imageBackground;

    [Range(0, 60)]
    public float amount = 0;

    private Text pieCircleText;


    private void Start()
    {
        pieCircleText = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        //if (amount < 100)
        //{
        //    pieCircleText.text = "0";
        //}

        //else if (amount >= 100)
        //{
        //    pieCircleText.text = "35";
        //}

        //if (GameManager.Instance.isGameStarted)
        //{
        //    amount += 1.66f * Time.deltaTime;
        //    float amountFillled = amount / 100f;
        //    imageBackground.fillAmount = amountFillled;
        //}
    }

    public void ProgessBar()
    {
        amount += 1.66f * Time.deltaTime;
        float amountFillled = amount / 100f;
        imageBackground.fillAmount = amountFillled;
    }
}
