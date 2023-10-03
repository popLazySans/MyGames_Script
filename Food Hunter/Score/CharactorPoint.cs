using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class CharactorPoint : NetworkBehaviour
{

    public int TargetPoint;

    public RandomObjectSpawner RandomScript;
    internal GameObject WinInterface;
    internal GameObject LoseInterface;
    internal GameObject EndPanel;
    internal GameObject GamePanel;
    private GameObject ResetManager;
    private ResetManager ResetScript;
    public int time;
    private BaseObject baseObject;
    private CanvasVariable canvasVariable;
    void Start()
    {
        canvasVariable = GameObject.Find("Canvas").GetComponent<CanvasVariable>();
        EndPanel = canvasVariable.EndPanel;
        WinInterface = canvasVariable.winInterface;
        LoseInterface = canvasVariable.loseInterFace;
        GamePanel = canvasVariable.gameInterface;
        RandomScript = gameObject.GetComponent<RandomObjectSpawner>();
       // ResetScript = ResetManager.GetComponent<ResetManager>();
        if (IsLocalPlayer)
        {
            StartCoroutine(timeCount());
        }
    }

    IEnumerator timeCount()
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time -= 1;
        }
    }
    void Update()
    {
         if(time == 0) {
            GamePanel.SetActive(false);
            if (ScoreText.whoWin == 1)
            {
                if (IsOwnedByServer)
                {
                    Win();
                }
                else
                {
                    Lose();
                }
            }
            else if(ScoreText.whoWin == 2)
            {
                if (IsOwnedByServer)
                {
                    Lose();
                }
                else
                {
                    Win();
                }
            }
            else
            {
                Win();
            }
        }
    }
    [ServerRpc]
    private void EndServerRpc()
    {
        EndClientRpc();
    }
    [ClientRpc]
    private void EndClientRpc()
    {
        if (IsOwner){Win();}
        else { Lose(); }
    }
    public void Playing()
    {
        EndPanel.SetActive(false);
    }
    public void Win()
    {
        EndPanel.SetActive(true);
        WinInterface.SetActive(true);
    }
    public void Lose()
    {
        EndPanel.SetActive(true);
        LoseInterface.SetActive(true);
    }

    public void receivePoint(int point)
    {
        TargetPoint += point;
    }

}
