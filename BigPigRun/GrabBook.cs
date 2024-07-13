using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
public class GrabBook : MonoBehaviour
{
    public SteamVR_Action_Boolean Action;
    PointManager pointManager = new PointManager();
    Interactable interactable;
    public GameObject bookUIgameObject;
    private AudioSource audioSource;
    bool isEnable;
    void OnEnable()
    {
        isEnable = false;
        interactable = GetComponent<Interactable>();
        bookUIgameObject.SetActive(isEnable);
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (interactable.attachedToHand != null)
        {
            SteamVR_Input_Sources source = interactable.attachedToHand.handType;
            if (Action[source].stateDown)
            {
                BookActive();
                audioSource.Play();
            }
        }
    }
    public void BookActive()
    {
        ShowBookUI();
    }
    public void ShowBookUI()
    {
        isEnable = !isEnable;
        bookUIgameObject.SetActive(isEnable);
    }
}
