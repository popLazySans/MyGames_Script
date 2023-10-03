using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class GaugeManager : NetworkBehaviour
{
    public SetDatatoPlayer setDatatoPlayer;
    public NetworkVariable<int> gaugeP1 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> gaugeP2 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    private Slider player1GaugeSlider;
    private Slider player2GaugeSlider;
    private Slider playerOwnerGaugeSlider;
    public int MaxGauge;
    void Start()
    {
        player1GaugeSlider = GameObject.Find("LeftGauge").GetComponent<Slider>();
        player2GaugeSlider = GameObject.Find("RightGauge").GetComponent<Slider>();
        playerOwnerGaugeSlider = GameObject.Find("OwnGauge").GetComponent<Slider>();
        StartCoroutine(GainGaugeOverTime());
    }
    private void Update()
    {
        ShowGaugeSlider();
    }
    public void ShowGaugeSlider()
    {
        if (IsOwnedByServer) 
        { 
            player1GaugeSlider.value = gaugeP1.Value;
            if (IsOwner)
            {
                playerOwnerGaugeSlider.value = playerOwnerGaugeSlider.maxValue - gaugeP1.Value;
            }
        }
        else { 
            player2GaugeSlider.value = gaugeP2.Value;
            if (IsOwner)
            {
                playerOwnerGaugeSlider.value = playerOwnerGaugeSlider.maxValue - gaugeP2.Value;
            }
        }
    }
    IEnumerator GainGaugeOverTime()
    {
        while (true)
        {
            if (IsLocalPlayer)
            {
                if (IsOwnedByServer) { if (gaugeP1.Value < MaxGauge) { if (setDatatoPlayer.CharacterID1.Value == 0) { gaugeP1.Value += 10; } else { gaugeP1.Value += 1; } } }
                else { if (gaugeP2.Value < MaxGauge) { if (setDatatoPlayer.CharacterID2.Value == 0) { gaugeP2.Value += 10; } else { gaugeP2.Value += 1; } } }
            }
            yield return new WaitForSeconds(1f);
        }
    }
    public void ReceiveGauge()
    {
        if (IsLocalPlayer)
        {
            if (IsOwnedByServer)
            {
                gaugeP1.Value = MaxGauge;
            }
            else
            {
                gaugeP2.Value = MaxGauge;
            }
        }
    }
}