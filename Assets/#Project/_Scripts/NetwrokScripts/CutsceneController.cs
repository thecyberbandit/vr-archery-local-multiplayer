using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public GameObject cam1;
    public GameObject mainCamera;


    public void PlayCutScene()
    {
        StartCoroutine(TheSequence());
    }

    IEnumerator TheSequence()
    {
        mainCamera.SetActive(false);
        cam1.SetActive(true);
        yield return new WaitForSeconds(25f);
        cam1.SetActive(false);
        mainCamera.SetActive(true);
        GameManager.Instance.isGameStarted = true;
    }
}
