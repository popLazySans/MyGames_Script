using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;
public class ScoreText : NetworkBehaviour
{
    public GameObject pointMananger;
    private CharactorPoint Point;
    private RandomObjectSpawner amout;
    private Text text;
    TMP_Text p1Text;
    TMP_Text p2Text;
    MainPlayerName mainPlayer;
    TMP_Text timeText;
    public NetworkVariable<int> scoreP1 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> scoreP2 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> time = new NetworkVariable<int>(30, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public static int whoWin;
    // Start is called before the first frame update
    void Start()
    {
        text =  gameObject.GetComponent<Text>();
        Point = pointMananger.GetComponent<CharactorPoint>();
        amout = pointMananger.GetComponent<RandomObjectSpawner>();
        p1Text = GameObject.Find("LeftScore").GetComponent<TMP_Text>();
        p2Text = GameObject.Find("RightScore").GetComponent<TMP_Text>();
        timeText = GameObject.Find("Time").GetComponent<TMP_Text>();
        mainPlayer = pointMananger.GetComponent<MainPlayerName>();
    }
    public void updateScore()
    {
        if (IsOwnedByServer) {  p1Text.text = $"{mainPlayer.playerNameA.Value}:{scoreP1.Value}"; }
        else {p2Text.text = $"{ mainPlayer.playerNameB.Value}:{scoreP2.Value}"; }
        timeText.text = time.Value.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        if (IsLocalPlayer)
        {
            time.Value = Point.time;
        }
        //text.text = Point.TargetPoint + " / " + amout.amoutObject;
        updateScore();
        if (IsLocalPlayer) 
        {
            if (IsOwnedByServer) 
            {
                scoreP1.Value = Point.TargetPoint;
                scoreP2.Value = GetNumberDataFromText(p2Text);
            }
            else
            {
                scoreP1.Value = GetNumberDataFromText(p1Text);
                scoreP2.Value = Point.TargetPoint;
            }
        }
        if (!IsOwner) return;
        if (scoreP1.Value >scoreP2.Value)
        {
            whoWin = 1;
        }
        else if(scoreP2.Value>scoreP1.Value)
        {
            whoWin = 2;
        }
        else
        {
            whoWin = 3;
        }

    }
    public int GetNumberDataFromText(TMP_Text text)
    {
        string[] splitData = text.text.Split(char.Parse(":"));
        if (splitData.Length == 2)
        {
          int data = int.Parse(splitData[1]);
          return data;
        }
        else
        {
          return 0;
        }
    } 
}
