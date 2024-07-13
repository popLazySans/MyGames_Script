using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalSceneActive : MonoBehaviour
{
    public int SceneNumber;
    public SceneStateMachine sceneState;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneNumber != sceneState.Scenenumber)
        {
            gameObject.SetActive(false);
        }
    }
}
