using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;
public class PlayerController : MonoBehaviour
{
    public SteamVR_Action_Vector2 input;
    public static float speed  = 2;
    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x,0,input.axis.y));
        Player.instance.trackingOriginTransform.position += speed * Time.deltaTime * Vector3.ProjectOnPlane(direction,Vector3.up);
    }
}
