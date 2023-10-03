using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMove : MonoBehaviour
{
    public GameObject Origin;
    public GameObject ObjectTo;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.position = Vector3.Slerp(gameObject.transform.position, ObjectTo.transform.position, speed); 
        gameObject.transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        if (gameObject.transform.position.x<-50)
        {
            gameObject.transform.position = Origin.transform.position;
        }
    }
}
