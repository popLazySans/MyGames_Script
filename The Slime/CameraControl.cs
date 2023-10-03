using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class CameraControl : MonoBehaviour
{
    public GameObject mainCam, charactor;
    public GameObject playerCam, SceneCam;
    public TMP_Text StartText;
    public Canvas canvas;
    CinemachineBrain cinemachineBrain;
    public static bool GameStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        StartText.SetText("Slain All Enemy");
        cinemachineBrain = mainCam.GetComponent<CinemachineBrain>();
        charactor.GetComponent<slimeMove>().enabled = false;
        canvas.GetComponent<pauseManager>().enabled = false;
        SceneCam.SetActive(true);
        playerCam.SetActive(false);
        StartCoroutine(finishToPlayer());
        Debug.Log(GameStarted);
    }

    // Update is called once per frame
    void Update()
    {
        if (!cinemachineBrain.IsBlending)
        {
            
            ICinemachineCamera finishCam = SceneCam.GetComponent<ICinemachineCamera>();
            bool finishLive = CinemachineCore.Instance.IsLive(finishCam);
            if (!finishLive && !GameStarted)
            {
                GameStarted = true;
                charactor.GetComponent<slimeMove>().enabled = true;
                StartCoroutine(TextWithDelay(StartText, "GO!!!!", 2f));
                if (Time.timeScale == 0)
                {
                    finishLive = false;
                    GameStarted = false;
                }
            }
        }
    }
    IEnumerator finishToPlayer()
    {
        yield return new WaitForSeconds(2);
        SceneCam.SetActive(false);
        playerCam.SetActive(true);

    }
    IEnumerator TextWithDelay(TMP_Text startText,string text, float delay)
    {
        startText.SetText(text);
        yield return new WaitForSeconds(delay);
        startText.SetText("Pause");
        startText.enabled = false;
        canvas.GetComponent<pauseManager>().enabled = true;
    }
    
}
