using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;
public class CameraControlPlayerDirection : NetworkBehaviour
{
    public GameObject CameraFollow;

    public CameraStatemachine cameraStatemachine;

    private float Camera_rotation_X;
    void Start()
    {
        cameraStatemachine = GetComponent<CameraStatemachine>();
    }

    void Update()
    {
        if (!IsOwner) return;
        cameraStatemachine.SetAtSame_PlayerPosition();
        ControlPlayerDirection();
    }
    public void ControlPlayerDirection()
    {
        SetRotation();
        CameraFollow.transform.rotation = Quaternion.Euler(0, Camera_rotation_X, 0);
    }
    public void SetRotation()
    {
        Camera_rotation_X += Input.GetAxis("Mouse X");
    }
}
