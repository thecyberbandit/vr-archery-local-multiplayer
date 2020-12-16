using UnityEngine;
using System.Collections;
using Mirror;

public class Arrow : NetworkBehaviour {

	private bool isAttached = false;

	private bool isFired = false;

    public float destroyAfter = 15f;



    public override void OnStartServer()
    {
        Invoke(nameof(DestroySelf), destroyAfter);
    }

    [Server]
    void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }

    void OnTriggerStay()
    {
		AttachArrow ();
	}

	void OnTriggerEnter()
    {
		AttachArrow ();
	}

    //[ServerCallback]
    //void OnTriggerEnter(Collider co)
    //{
    //    if (co.CompareTag("gy"))
    //    {
    //        //NetworkServer.Destroy(co.gameObject);
    //        NetworkServer.Destroy(co.gameObject);
    //    }
    //}

    void Update()
    {
        if (isFired && transform.GetComponent<Rigidbody> ().velocity.magnitude > 5f)
        {
			transform.LookAt (transform.position + transform.GetComponent<Rigidbody> ().velocity);
		}
	}

	public void Fired()
    {
		isFired =  true; 
	}

	private void AttachArrow()
    {
		var device = SteamVR_Controller.Input((int)ArrowManager.Instance.trackedObj.index);

		if (!isAttached && device.GetTouch (SteamVR_Controller.ButtonMask.Trigger))
        {
			ArrowManager.Instance.AttachBowToArrow ();
			isAttached = true;
		}
	}
}
