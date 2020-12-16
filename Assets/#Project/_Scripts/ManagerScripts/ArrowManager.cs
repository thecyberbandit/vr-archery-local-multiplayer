using UnityEngine;
using System.Collections;
using Mirror;

public class ArrowManager : MonoBehaviour {

	public static ArrowManager Instance;

	public GameObject stringAttachPoint;
	public GameObject arrowStartPoint;
	public GameObject stringStartPoint;

	public GameObject arrowPrefab;

    public SteamVR_TrackedObject trackedObj;

    private GameObject currentArrow;

    private bool isAttached = false;
    private bool playStringSound;

    public bool host;


	void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
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
        playStringSound = false;
        
        //trackedObj = ViveManager.instance.rightHand.GetComponent<SteamVR_TrackedObject>();
    }

    bool HostType_Arrow()
    {
        if (HostType.hostType.type == HostType.PlayerType.host)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    void Update()
    {
        PlayerReady();
		AttachArrow ();
		PullString ();
    }

    private void PlayerReady()
    {
        host = HostType_Arrow();

        var device = SteamVR_Controller.Input((int)trackedObj.index);

        if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (host)
            {
                GameObject.FindGameObjectWithTag("player1").GetComponent<PlayerManager>().ServerReady();
                Debug.Log("Server is ready");
            }

            else
            {
                GameObject.FindGameObjectWithTag("player2").GetComponent<PlayerManager>().ClientReady();
                GameManager.Instance.clientReady = true;
            }
        }
    }

	private void PullString()
    {
		if (isAttached)
        {
            float dist = (stringStartPoint.transform.position - this.transform.position).magnitude; //trackedObj.transform.position;

            if (dist > 0 && !playStringSound)
            {
                AudioManager.instance.PlaySoundOnce("string");
                playStringSound = true;
            }

			stringAttachPoint.transform.localPosition = stringStartPoint.transform.localPosition  + new Vector3 (5f* dist, 0f, 0f);

            var device = SteamVR_Controller.Input((int)trackedObj.index);

			if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger))
            {
                AudioManager.instance.StopSound("string");
                Fire();
            }
		}
	}

	private void Fire()
    {
        playStringSound = false;
        AudioManager.instance.PlaySound("arrowshoot");

        float dist = (stringStartPoint.transform.position - this.transform.position).magnitude;

        currentArrow.transform.parent = null;
		currentArrow.GetComponent<Arrow> ().Fired ();

		Rigidbody r = currentArrow.GetComponent<Rigidbody> ();
		r.velocity = currentArrow.transform.forward * 50f * dist;
		r.useGravity = true;


        Vector3 vel = r.velocity;
        float angle = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;

        currentArrow.transform.eulerAngles = new Vector3(0, 0, angle);

        currentArrow.GetComponent<Collider> ().isTrigger = false;

		stringAttachPoint.transform.position = stringStartPoint.transform.position;

		currentArrow = null;
		isAttached = false;
	}

	private void AttachArrow()
    {
        host = HostType_Arrow();

        if (currentArrow == null && GameManager.Instance.isGameStarted)
        {
            //currentArrow = Instantiate(arrowPrefab);
            ////Vector3 currentScale = currentArrow.transform.localScale;
            //currentArrow.transform.parent = trackedObj.transform;
            ////currentArrow.transform.localScale = currentScale * 1.5f;
            //currentArrow.transform.localPosition = new Vector3(0f, 0f, .2f);
            //currentArrow.transform.localRotation = Quaternion.identity;
            

            currentArrow = Instantiate(arrowPrefab);

            if (host)
            {
                currentArrow.tag = "p1";
            }

            else
            {
                currentArrow.tag = "p2";
            }

            currentArrow.transform.parent = trackedObj.transform;
            currentArrow.transform.localPosition = new Vector3(0f, 0f, .342f);
            currentArrow.transform.localRotation = Quaternion.identity;

            

            //if (host)
            //{
            //    currentArrow = NetworkManager.singleton.players[0].GetComponent<PlayerManager>().SpawnArrow();
            //    currentArrow.tag = "p1";

            //    currentArrow.transform.parent = trackedObj.transform;
            //    //currentArrow.transform.localScale = currentScale * 1.5f;
            //    currentArrow.transform.localPosition = new Vector3(0f, 0f, .2f);
            //    currentArrow.transform.localRotation = Quaternion.identity;
            //}

            //else
            //{
            //    currentArrow = NetworkManager.singleton.players[1].GetComponent<PlayerManager>().SpawnArrow();
            //    currentArrow.tag = "p2";

            //    currentArrow.transform.parent = trackedObj.transform;
            //    //currentArrow.transform.localScale = currentScale * 1.5f;
            //    currentArrow.transform.localPosition = new Vector3(0f, 0f, .2f);
            //    currentArrow.transform.localRotation = Quaternion.identity;
            //}
        }
        //if (GameManager.Instance.isGameStarted)
        //{
        //    if (currentArrow == null)
        //    {
        //        currentArrow = Instantiate(arrowPrefab);
        //        currentArrow.transform.parent = trackedObj.transform;
        //        currentArrow.transform.localPosition = new Vector3(0f, 0f, .342f);
        //        currentArrow.transform.localRotation = Quaternion.identity;

        //        if (host.isHost)
        //        {
        //            currentArrow.tag = "p1";
        //        }

        //        else
        //        {
        //            currentArrow.tag = "p2";
        //        }
        //    }
        //}
    }

    

    public void AttachBowToArrow()
    {
		currentArrow.transform.parent = stringAttachPoint.transform;
		currentArrow.transform.position = arrowStartPoint.transform.position;
		currentArrow.transform.rotation = arrowStartPoint.transform.rotation;

        AudioManager.instance.PlaySound("attach");

		isAttached = true;
	}

    public void PlayStringSound()
    {
        if (playStringSound)
        {
            AudioManager.instance.PlaySound("string");
            playStringSound = false;
        }
    }
}
