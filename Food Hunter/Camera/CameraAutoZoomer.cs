using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CameraAutoZoomer : NetworkBehaviour
{
    public CameraStatemachine cameraStatemachine;
    private GameObject Camera;

    bool isCovered;
    RaycastHit hitInfomation;
    float camtoplayerDistance;
    float raytocamDistance;
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        cameraStatemachine = GetComponent<CameraStatemachine>();
        Camera = cameraStatemachine.Target_Camera;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        cameraStatemachine.CameraRaycast();
        isCovered = cameraStatemachine.IsHitted();
        hitInfomation = cameraStatemachine.hitInfomation();
        if(isCovered == true)
        {
            ZoomIn();
        }
        else
        {
            ZoomOut();
        }
    }
    public void ZoomIn()
    {
        direction = cameraStatemachine.returnDirection();
        camtoplayerDistance = (Camera.transform.position - cameraStatemachine.Target_Player.transform.position).magnitude;
        raytocamDistance = (cameraStatemachine.Target_Raycaster.transform.position - Camera.transform.position).magnitude;
        Debug.DrawRay(Camera.transform.position,direction*(camtoplayerDistance-0.6f),Color.blue);
        Debug.DrawRay(cameraStatemachine.Target_Raycaster.transform.position, direction * (raytocamDistance-0.1f), Color.red);
        if (Physics.Raycast(Camera.transform.position, direction, camtoplayerDistance - 0.6f ) == true)
        {
            Camera.transform.Translate(0,0,20f*Time.deltaTime);
        }
        else if (Physics.Raycast(cameraStatemachine.Target_Raycaster.transform.position,direction,raytocamDistance-0.1f))
        {
            Camera.transform.Translate(0, 0, -20f * Time.deltaTime);
        }
    }
    public void ZoomOut()
    {
       if(Camera.transform.position == cameraStatemachine.Target_Raycaster.transform.position)
        {

        }
        else
        {
            Camera.transform.position = Vector3.Lerp(Camera.transform.position, cameraStatemachine.Target_Raycaster.transform.position, 20f * Time.deltaTime);
        }
    }
}
