using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Loading : MonoBehaviour
{
    private float lagThreshold = 0.1f;
    private float StartTime;
    private float ExecutionTime;
    public GameObject LoadingPanel;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        StartTime = Time.realtimeSinceStartup;
        ExecutionTime = Time.realtimeSinceStartup - StartTime;
        LoadingPanel.SetActive(isLoading());
    }
    public bool isLoading()
    {
        if (ExecutionTime > lagThreshold)
        {
            Debug.Log("lagging");
            return true;
        }
        else
        {
            return false;
        }
    }
}
