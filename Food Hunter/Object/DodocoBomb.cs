using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodocoBomb : MonoBehaviour
{
    public float ObjectSpeed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ObjectMove();
    }
    public void ObjectMove()
    {
        transform.Translate(Vector3.forward*ObjectSpeed*Time.deltaTime);
    }
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<SetDatatoPlayer>().CharacterID1.Value != 2 && collision.gameObject.GetComponent<SetDatatoPlayer>().CharacterID2.Value != 2)
        {
           collision.gameObject.GetComponent<PlayerMovements>().Onstunned();
        }
    }
}
