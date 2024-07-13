using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedHandler : MonoBehaviour
{
    public KATDevice kATDevice;
    // Start is called before the first frame update
    void Start()
    {
        kATDevice = gameObject.GetComponent<KATDevice>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void speedDelay(float delayValue)
    {
        PlayerController.speed = 2f - (delayValue / 100);
        kATDevice.multiply = 1.2f - (delayValue / 100);
    }
    public void stop()
    {
        kATDevice.multiply = 0;
    }
}
