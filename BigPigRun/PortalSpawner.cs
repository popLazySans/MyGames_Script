using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    public GameObject portalObject;
    private TimeCounter timeManager;
    public int timeToSpawn;
    private ShowText showText;
    // Start is called before the first frame update
    void Start()
    {
        showText = GameObject.FindGameObjectWithTag("ShowText").GetComponent<ShowText>();
        timeManager = GetComponent<TimeCounter>();
        portalObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if it is game.
        if (180 - timeManager.timeValue == timeToSpawn)
        {
            portalObject.SetActive(true);
            showText.ShowSpawnPortal();
        }

        //test
        //portalObject.SetActive(true);
    }
}
