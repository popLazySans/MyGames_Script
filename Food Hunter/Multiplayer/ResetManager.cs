using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ResetManager : MonoBehaviour
{
    // Start is called before the first frame update
    private LoginManager loginManager;
    void Start()
    {
        loginManager = GameObject.Find("LoginManager").GetComponent<LoginManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResetGame()
    {
        loginManager.newGame();
    }
}
