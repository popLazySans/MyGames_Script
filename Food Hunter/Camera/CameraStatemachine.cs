using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStatemachine : MonoBehaviour
{


    public GameObject Target_Player;
    public GameObject Target_Raycaster;
    public GameObject Target_Camera;

    bool isHit;
    RaycastHit hit;

    Vector3 heading;
    float distance;
    Vector3 direction;
    // Start is called before the first frame update

    public void SetAtSame_PlayerPosition()
    {
        gameObject.transform.position = Target_Player.transform.position;
    }
    public void CameraRaycast()
    {
        CameraDirectionCalculator();
        isHit = Physics.Raycast(Target_Raycaster.transform.position, direction,out hit, distance-0.6f);
        //Debug.Log("Hit = " + isHit);
        //Debug.DrawRay(Target_Raycaster.transform.position, direction * (distance-0.6f), Color.green);
    }
    public bool IsHitted()
    {
        return isHit;
    }
    public RaycastHit hitInfomation()
    {
        return hit;
    }


    public void CameraDirectionCalculator()
    {
        heading = Target_Player.transform.position - Target_Raycaster.transform.position;
        distance = heading.magnitude;
        direction = heading / distance;
        
    }
    public Vector3 returnDirection()
    {
        return direction;
    }


}
