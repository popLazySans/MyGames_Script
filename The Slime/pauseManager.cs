using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class pauseManager : MonoBehaviour
{
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unpaused;
    public TMP_Text pauseText;
    public GameObject ADbutton;
    public GameObject retryButton;
    public GameObject homeButton;
    Canvas canvas;
    // Start is called before the first frame update
    void Awake()
    {
        homeButton.SetActive(false);
        retryButton.SetActive(false);
        ADbutton.SetActive(false);
        canvas = GetComponent<Canvas>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
           // canvas.enabled = !canvas.enabled;
            
            Pause();
        }
    }
    public void Pause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        if (Time.timeScale == 0)
        {
            retryButton.SetActive(true);
            homeButton.SetActive(true);
            pauseText.enabled = !pauseText.enabled;
            ADbutton.SetActive(pauseText.enabled);
            paused.TransitionTo(0.01f);
            

        }
        else if (Time.timeScale == 1){
            retryButton.SetActive(false);
            homeButton.SetActive(false);
            pauseText.enabled = !pauseText.enabled;
            ADbutton.SetActive(pauseText.enabled);
            unpaused.TransitionTo(0.01f);
            
        }
    }
}
