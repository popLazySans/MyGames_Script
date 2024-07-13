using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class GUIactived : MonoBehaviour
{
    public SteamVR_Action_Boolean GUIAction;
    public GameObject GUIgameobject;
    private bool isGUIactived;
    // Start is called before the first frame update
    void Start()
    {
        isGUIactived = true;
        ClickGUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (GUIAction.stateDown)
        {
            ClickGUI();
        }
    }
    public void ClickGUI()
    {
        isGUIactived = !isGUIactived;
        GUIgameobject.SetActive(isGUIactived);
    }
}
