using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreSave : MonoBehaviour
{
    private List<int> scoreSaved = new List<int>();
    private ShowText showText;
    //private DistanceToPoint distanceTo;
    // Start is called before the first frame update
    void OnEnable()
    {
        // distanceTo = GameObject.FindGameObjectWithTag("DistancePath").GetComponent<DistanceToPoint>();
        showText = GameObject.FindGameObjectWithTag("ShowText").GetComponent<ShowText>();
        LoadDatatoList();
    }

    // Update is called once per frame
    void Update()
    {
        showText.ShowScoreText(scoreSaved);
    }
    public void SaveCurrentScoreToList(int score)
    {
        Debug.Log("Saved");
        scoreSaved.Add(score);
        rankingSet();
        SaveDataToPref();
    }
    public void SaveDataToPref()
    {
        int i = 0;
        string scoreCoding = "";
        foreach (int allscore in scoreSaved)
        {
            i++;
            scoreCoding += allscore+"_";
            Debug.Log("Save "+ i+ ". " +allscore);
        }
        PlayerPrefs.SetString("Point", scoreCoding);
        Debug.Log("Save ID : "+ scoreCoding);
    }
    public void LoadDatatoList()
    {
        string[] Data = PlayerPrefs.GetString("Point").Split(char.Parse("_"));
        foreach (string data in Data)
        {
            if(data != "")
            {
                scoreSaved.Add(Convert.ToInt32(data));
                rankingSet();
            }
        }
    }
    public void rankingSet()
    {
        List<int> rankList =  scoreSaved.OrderByDescending(x => x).ToList();
        scoreSaved = rankList;
    }
    static int SortByScore(int x,int y){
        return x.CompareTo(y);
    }
}
