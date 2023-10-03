using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TobeContinueScene : MonoBehaviour
{
    public static int defeated_count = 0;
    public GameObject mainTimelineGameObject;
    public GameObject afterWinTimelineGameObject;
    public GameObject logoGameObject;
    public GameObject textGameObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (defeated_count == 2)
        {
            logoGameObject.SetActive(true);
            textGameObject.SetActive(true);
            mainTimelineGameObject.SetActive(false);
            afterWinTimelineGameObject.SetActive(true);
        }
    }
}
