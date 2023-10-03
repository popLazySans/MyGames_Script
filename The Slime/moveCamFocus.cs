using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamFocus : MonoBehaviour
{
    
    public GameObject cam1;
    public GameObject cam2;
    public GameObject charactor;
    public static bool cameraLock = true;
    public Canvas canvas;
    // Start is called before the first frame update
    void Awake()
    {
        gameObject.GetComponent<CameraControl>().enabled = false;
        charactor.GetComponent<slimeMove>().enabled = false;
        cam1.SetActive(true);
        cam2.SetActive(false);
        

    }
    void Start()
    {
        canvas.GetComponent<pauseManager>().enabled = false;
        StartCoroutine(moveCamera(cam1, cam2, 5f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator moveCamera(GameObject camera1,GameObject camera2,float timer)
    {
        yield return new WaitForSeconds(0.01f);
        camera1.SetActive(false);
        camera2.SetActive(true);
        yield return new WaitForSeconds(timer);
        gameObject.GetComponent<CameraControl>().enabled = true;
        cameraLock = false;
        charactor.GetComponent<slimeMove>().enabled = true;

    }
}
