using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject TUI;
    public bool GameActived = false;
    // Start is called before the first frame update
    void Start()
    {
        TUI.SetActive(false);
      
    }

    // Update is called once per frame
    void Update()
    {
        GameActived = CameraControl.GameStarted;
        if(GameActived == true)
        {
            TUI.SetActive(true);
        }
    }
}
