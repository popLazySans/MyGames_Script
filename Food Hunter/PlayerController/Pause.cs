using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using UnityEngine.InputSystem;
public class Pause : NetworkBehaviour
{
    public Button resumeButton;
    public GameObject exitPanel;
    public bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!IsOwner) return;
        exitPanel = GameObject.Find("ExitPanel");
        exitPanel.SetActive(isPaused);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        if (GameObject.Find("Resume")!=null)
        {
            resumeButton = GameObject.Find("Resume").GetComponent<Button>();
        }
        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(PauseGame);
        }
    }
    public void OnPause(InputAction.CallbackContext context)
    {
        if (!IsOwner) return;
        if (context.started)
        {
            PauseGame();
        }
        else
        {
            Debug.Log("Err");
            Debug.Log(context.ReadValue<float>());
        }
    }
    public void PauseGame()
    {
        if (!IsOwner) return;
        isPaused = !isPaused;
        exitPanel.SetActive(isPaused);
    }
}
