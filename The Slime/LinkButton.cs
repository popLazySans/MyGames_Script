using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LinkButton : MonoBehaviour
{
    private int currentLevel;
    pauseManager pause;
    slimeMove move;
    public GameObject slime;

    // Start is called before the first frame update
    void Start()
    {
        if (slime != null)
        {
            move = slime.GetComponent<slimeMove>();
        }
        pause = GetComponent<pauseManager>();
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Replay()
    {
        Time.timeScale = 1;
        CameraControl.GameStarted = false;
        Current.Score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log(Time.timeScale);

    }
    public void Skip()
    {
        SceneManager.LoadScene("Gameplay");
    }
    public void PauseButton()
    {
        if (CameraControl.GameStarted == true)
        {
            pause.Pause();
        }
       
    }
    public void UpButton()
    {
        move.Jump();
    }
    public void LeftButtonDown()
    {
        move.leftCommand = true;
    }
    public void LeftButtonUP()
    {
        move.leftCommand = false;
    }
    public void RightButtonDown()
    {
        move.rightCommand = true;
    }
    public void RightButtonUP()
    {
        move.rightCommand = false;
    }
    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
    public void StartStory()
    {
        StartCoroutine(delayStart()); 
    }
    public void Exit()
    {
        Application.Quit();
    }
    /*public void YODstyle()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(currentLevel);
        
    }*/
    IEnumerator delayStart()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("StoryTelling");
    }
}
