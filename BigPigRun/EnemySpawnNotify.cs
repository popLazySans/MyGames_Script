using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnNotify : MonoBehaviour
{
    private ShowText showText;
    
    // Start is called before the first frame update
    void Start()
    {
        showText = GameObject.FindGameObjectWithTag("ShowText").GetComponent<ShowText>();
        showText.ShowSpawnEnemyText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
