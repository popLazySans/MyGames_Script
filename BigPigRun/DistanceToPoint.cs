using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceToPoint : MonoBehaviour
{
    public float distance;
    public float sumDistance;
    public int prevPoint;
    public int currentPoint;
    private ShowText showText;
    // Start is called before the first frame update
    void Start()
    {
        showText = GameObject.FindGameObjectWithTag("ShowText").GetComponent<ShowText>();
    }
    public void FixedUpdate()
    {
        showText.ShowDistanceText((int)distance);
    }
    public void getDistance(float dist)
    {
        distance += dist;
    }
    public void distToPoint(int sceneMultiply)
    {
        prevPoint = currentPoint;
        currentPoint += (int)distance*sceneMultiply;
        sumDistance += distance;
        //distance = 0f;
    }
    public void resetDistance()
    {
        distance = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
