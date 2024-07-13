using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUpdate : MonoBehaviour
{
    private Light light;
    // Start is called before the first frame update
    void Start()
    {
        light = gameObject.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateLight(int vision)
    {
        light.range = 3+vision;
    }
  
}
