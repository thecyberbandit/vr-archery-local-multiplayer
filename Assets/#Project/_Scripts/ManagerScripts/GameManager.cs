using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mirror;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public GameObject countdown;
    //public Text countdownText;

    public float timeLeft = 90f;

    public Text timerText;
    public RadialProgressBar bar;

    public Animator anim;

    public GameObject winImage;
    public GameObject looseImage;
    public GameObject drawImage;

    [HideInInspector]
    public bool isGameStarted;
    [HideInInspector]
    public bool isGameOver;

    public GameObject playBoard;

    private Animator countdownAnim;

    public AnimationClip cutsceneClip;

    public bool serverReady, clientReady;

    public int player1Score = 0, player2Score = 0;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        //winImage.SetActive(false);
        //looseImage.SetActive(false);
        //drawImage.SetActive(false);
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        isGameStarted = false;
        isGameOver = false;
        serverReady = false;
        clientReady = false;

        countdownAnim = countdown.GetComponent<Animator>();

        StartGame();
    }

    private void StartGame()
    {
        //StartCoroutine(OneTwoThree());

       

        AudioManager.instance.PlaySound("background");

        //scoreText.text = "Score: " + score.ToString();
    }

    private void Update()
    {
        if (serverReady &&  !isGameStarted && !isGameOver)
        {
            playBoard.SetActive(false);
            StartCoroutine(TheSequence());
        }

        if (isGameStarted)
        {
            bar.amount += 1.111f * Time.deltaTime;
            float amountFillled = bar.amount / 100f;
            bar.imageBackground.fillAmount = amountFillled;

            int temp = (int)timeLeft;
            timerText.text = temp.ToString();

            Debug.Log(timerText.text);

            timeLeft -= Time.deltaTime;
        }

        if (timeLeft < 0)
        {
            timerText.text = "0";
            isGameStarted = false;
            isGameOver = true;
        }

        if (isGameOver)
        {
            if (HostType.hostType.type == HostType.PlayerType.host)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    isGameOver = false;
                    NetworkManager.singleton.StopHost();
                    NetworkManager.singleton.StopClient();
                    NetworkManager.singleton.ServerChangeScene("Main");
                }
            }
        }
    }

    IEnumerator TheSequence()
    {
        anim.SetBool("cutscene", true);
        yield return new WaitForSeconds(cutsceneClip.length);
        //anim.SetBool("cutscene", false);

        anim.SetBool("cutscene", false);

        Countdown();
        //isGameStarted = true;
    }

    public void Countdown()
    {
        //AudioManager.instance.PlaySound("countdown");
        StartCoroutine(OneTwoThree());
    }

    IEnumerator OneTwoThree()
    {
        //AudioManager.instance.PlaySound("countdown");

        yield return new WaitForSeconds(1f);

        countdownAnim.SetTrigger("count");

        yield return new WaitForSeconds(7f);

        isGameStarted = true;
    }
}
