using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateDistancePath : MonoBehaviour
{
    internal bool isPlaying;
    public GameObject targetPosGameObject;
    private DistanceToPoint distanceToPoint;
    //public GameObject doctorGameObject;
    // Start is called before the first frame update
    void OnEnable()
    {
        distanceToPoint = GetComponent<DistanceToPoint>();
        InvokeRepeating("updatePosition",1f,0.5f);
    }
    private void OnDisable()
    {
        updatePosition();
        CancelInvoke("updatePosition");
    }
    public void updatePosition()
    {
        float dist = Vector3.Distance(targetPosGameObject.transform.position,transform.position);
        if(isPlaying == true)
            distanceToPoint.getDistance(dist);
        gameObject.transform.position = targetPosGameObject.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        //doctorGameObject.SetActive(!isPlaying);
    }
}
