using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;
using UnityEngine.InputSystem;
public class UltimateSkill : NetworkBehaviour
{
    GaugeManager gaugeManager;
    SetDatatoPlayer characterData;
    UltimateClass ultimateClass;
    private bool isCanUseUltimate = false;
    private void Start()
    {
        ultimateClass = GetComponent<UltimateClass>();
        gaugeManager = GetComponent<GaugeManager>();
        characterData = ultimateClass.characterData;
    }
    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        if (gaugeManager.gaugeP1.Value == gaugeManager.MaxGauge || gaugeManager.gaugeP2.Value == gaugeManager.MaxGauge)
        {
            isCanUseUltimate = true;
        }
    }

    public void useUltimate (InputAction.CallbackContext context)
    {
        if (!IsOwner) return;
        if(context.started && isCanUseUltimate == true)
        {
            AllowUltimate();
           
        }
    }
    public void AllowUltimate()
    {
        thisUltimateActived();
        SetGaugeToZero();
    }
    public void SetGaugeToZero()
    {
        gaugeManager.gaugeP1.Value = 0;
        gaugeManager.gaugeP2.Value = 0;
        isCanUseUltimate = false;
    }
    public void thisUltimateActived()
    {
        if (IsOwnedByServer)
        {
            UltimateSelecter(characterData.CharacterID1.Value);
        }
        else
        {
            UltimateSelecter(characterData.CharacterID2.Value);
        }
    }
    public void UltimateSelecter(int id)
    {
        switch (id)
        {
            case 0:
                ultimateClass.Ultimate_1();
                break;
            case 1:
                ultimateClass.Ultimate_2();
                break;
            case 2:
                ultimateClass.Ultimate_3();
                break;
        }
    }
}
