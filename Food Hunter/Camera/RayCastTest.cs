using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastTest : MonoBehaviour
{
    public GameObject Target_Player;
    public GameObject Target_Cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraRaycast();
    }
    public void CameraRaycast()
    {
        Vector3 heading = Target_Player.transform.position - Target_Cam.transform.position;
        float distance = heading.magnitude;
        var direction = heading / distance;
        bool isTouch = Physics.Raycast(Target_Cam.transform.position, direction, distance);
        Debug.Log(isTouch);
        Debug.DrawRay(Target_Cam.transform.position, direction*distance, Color.green);

    }
}
