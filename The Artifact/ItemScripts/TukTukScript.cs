using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TukTukScript : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("destroyTukTuk", 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void destroyTukTuk()
    {
        Destroy(gameObject);
    }
    
}
