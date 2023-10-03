using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryCam : MonoBehaviour
{
    public GameObject[] gameObjectlist;
    private int numberArray = -1;
    public bool isCutSceneStarted = false;

    public GameObject fadeManager;
    fadeManager fade;
    // Start is called before the first frame update
    void Start()
    {
        fade = fadeManager.GetComponent<fadeManager>();
        numberArray = -1;
        StartCoroutine(Reading());
    }

    // Update is called once per frame
    void Update()
    {
        if (isCutSceneStarted == true)
        {
            gameObjectlist[numberArray].SetActive(false);
        }
        if (numberArray == gameObjectlist.Length-1)
        {
            StartCoroutine(ToAnotherScene());
        }
    }
    
    IEnumerator Reading()
    {
        for (int i = 0;i<gameObjectlist.Length;i++)
        {
            if(i == gameObjectlist.Length - 1)
            {
                fade.fadeOutToAnotherScene();
            }
            yield return new WaitForSeconds(3);
            numberArray += 1;
            isCutSceneStarted = true;
        }
        
    }
    IEnumerator ToAnotherScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Gameplay");
    }
    
}
