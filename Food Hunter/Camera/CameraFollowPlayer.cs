using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;
public class CameraFollowPlayer : NetworkBehaviour
{
    public GameObject CameraFollow;

    public CameraStatemachine cameraStatemachine;

    private float Camera_rotation_X;
    private float Camera_rotation_Y;
    void Start()
    {
        cameraStatemachine = GetComponent<CameraStatemachine>();
       
    }

    void Update()
    {
        if (!IsOwner) { gameObject.SetActive(false); return; }
        cameraStatemachine.SetAtSame_PlayerPosition();
        CameraMoveAroundPlayer();

    }
    //Can Change to InputSystem 
    public void CameraMoveAroundPlayer()
    {
        SetRotation();
        CameraFollow.transform.rotation = Quaternion.Euler(Camera_rotation_Y,Camera_rotation_X,0);
    }
    public void SetRotation()
    {
        Camera_rotation_X += Input.GetAxis("Mouse X");
        Camera_rotation_Y -= Input.GetAxis("Mouse Y");
    }
}
