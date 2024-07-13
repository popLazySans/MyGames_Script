using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BalanceBarColor : MonoBehaviour
{
    private PointManager pointManager;
    public  Image pointBar;
    public Image valueBar;
    // Start is called before the first frame update
    void Start()
    {
        pointManager = gameObject.GetComponent<PointManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pointManager.Point >= 1000 && pointManager.Point <= 3000){
            pointBar.color = new Color32(194,250,185,130);
            valueBar.color = new Color32(33,154,47,255);
        }
        else{
            pointBar.color = new Color32(250,185,194,130);
            valueBar.color = new Color32(225,92,40,255);
        }
    }
}
