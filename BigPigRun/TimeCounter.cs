using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class TimeCounter : MonoBehaviour
{
    public int time;
    internal int timeValue;
    public TMP_Text timeText ;
    public Slider timeSlider;
    // Start is called before the first frame update
    void Start()
    {
        timeSlider.maxValue = time;
    }
    private void OnEnable()
    {
        timeValue = time;
        StartCoroutine(timeCount());
    }
    // Update is called once per frame
    void Update()
    {
        //timeText.text = "Time : " +time.ToString();
        timeSlider.value = timeValue;
    }
    IEnumerator timeCount()
    {
        int timeReduced = 0;
        while (timeValue>0)
        {
            yield return new WaitForSeconds(1f);
            timeValue -= 1;
            timeReduced++;
            if(timeReduced%30 == 0){
                ShowTime();
                StartCoroutine(SetSpawnFalse());
            }
        }
        if (timeValue == 0)
        {
            timeText.text = "";
            SceneManagers sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagers>();
            sceneManager.LoseActive();
        }

    }
     public void ShowTime(){
        timeText.text = "Time remaining "+timeValue+" second";
    }
    IEnumerator SetSpawnFalse()
    {
        yield return new WaitForSeconds(8f);
        timeText.text = "";
    }
}
