using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
public class StaminaValue : NetworkBehaviour
{
    public int stamina;
    public int stamina_Max;
    private PlayerMovements playerMovements;
    public GameObject staminaSliderPrefab;
    Slider staminaSlider;
    // Start is called before the first frame update
    void Start()
    {
        playerMovements = gameObject.GetComponent<PlayerMovements>();
    }
    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;
        GameObject GUI = GameObject.FindWithTag("GUI");
        staminaSliderPrefab =  Instantiate(staminaSliderPrefab);
        staminaSliderPrefab.transform.SetParent(GUI.transform);
        staminaSlider = staminaSliderPrefab.GetComponent<Slider>();
        staminaSlider.maxValue = stamina_Max;
        staminaSlider.value = stamina;
    }
    // Update is called once per frame
    public bool staminaCanDrop()
    {
        if (stamina <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public bool staminaCanRecover()
    {
        if (stamina < stamina_Max)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void staminaDrop()
    {
        stamina -= 1;
    }
    public void staminaRecovery()
    {
        stamina += 1;
    }
    private void Update()
    {
        if (!IsOwner) return;
        staminaSlider.value = stamina;
    }
}
