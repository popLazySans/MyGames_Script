using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafollow : MonoBehaviour
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        if (target == null)
        {
            gameObject.transform.position = gameObject.transform.position;
        }
        else
        {
            gameObject.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, gameObject.transform.position.z);
        }
    }
}
