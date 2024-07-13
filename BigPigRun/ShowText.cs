using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShowText : MonoBehaviour
{
    public TMP_Text statText;
    public TMP_Text eventText;
    public TMP_Text distanceText;
    public GameObject winPanel;
    public GameObject losePanel;
    public TMP_Text scoreListText;
    public TMP_Text winloseText;
    private void Start()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }
    public void ShowPassText(float distance,int sceneMultiply,int prevPoint,int currentPoint)
    {
        if (statText != null)
            statText.text = "Next Level\nYou run "+distance+" m\nYou have point "+prevPoint+" + "+distance+" * "+sceneMultiply+" = "+currentPoint;
        Debug.Log("Win");
        StartCoroutine(SetSpawnFalse(statText));
    }
    public void ShowWinText(float distance, int sceneMultiply, int prevPoint, int currentPoint)
    {
        winPanel.SetActive(true);
        if (winloseText != null)
            winloseText.text = "You Clear All!!!!!!\nYou run " + distance + " m\nYou have point " + prevPoint + " + " + distance + " * " + sceneMultiply + " =\n\n" + currentPoint+" Point!!!!!!";
        Debug.Log("Win");
        StartCoroutine(SetSpawnFalse(winloseText));
        StartCoroutine(SetWinFalse());
    }
    public void ShowLoseText(float distance, int sceneMultiply, int prevPoint, int currentPoint)
    {
        losePanel.SetActive(true);
      if (winloseText != null)
            winloseText.text = "You Lose\nYou run " + distance + " m\nYou have point " + prevPoint + " + " + distance + " * " + sceneMultiply + " = \n\n" + currentPoint+" Point";
      Debug.Log("Lose");
      StartCoroutine(SetSpawnFalse(winloseText));
      StartCoroutine(SetWinFalse());
    }
     public void ShowTryAgainText()
    {
      if (statText != null)
            statText.text = "Please Balance Your Energy.";
      Debug.Log("Balanced");
      StartCoroutine(SetSpawnFalse(statText));
    }
    public void ShowSpawnEnemyText()
    {
        if (eventText != null)
            eventText.text = "Bird has arrived!!!";
        Debug.Log("Spawn Bird");
        StartCoroutine(SetSpawnFalse(eventText));
    }
    public void ShowSpawnPortal()
    {
        if (eventText != null) 
            eventText.text = "Portal has spawned!!!";
        Debug.Log("Spawn Portal");
        StartCoroutine(SetSpawnFalse(eventText));
    }
    public void ShowDistanceText(int dist)
    {
        if (distanceText != null)
            distanceText.text = dist + " m";
    }
    public void ShowScoreText(List<int>ScoreList)
    {
        int i = 1;
          scoreListText.text = "";
        if (scoreListText != null)
            foreach (int score in ScoreList)
            {
            scoreListText.text += i + ")      "+score+" m\n";
                i++;
            }
    }
    public void ShowFoodDetail(TMP_Text pageText,string name,string detail,int energy,int fat,int vitamin)
    {
        if (name == "Mushroom?")
        {
            pageText.text = "Food name : " + name + "\nEnergy : " + "???" + "\nVitamin : " + "???" + "\nTransfat : " + "???" +"\nDetail : "+ detail;
        }
        else
        {
            pageText.text = "Food name : " + name + "\nEnergy : " + energy + "\nVitamin : " + vitamin + "\nTransfat : "+fat+ "\nDetail : " + detail;
        }
    }
    IEnumerator SetSpawnFalse(TMP_Text text)
    {
        yield return new WaitForSeconds(8f);
        text.text = "";
        //winPanel.SetActive(false);
    }
    IEnumerator SetWinFalse()
    {
        yield return new WaitForSeconds(8f);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }
   
}
