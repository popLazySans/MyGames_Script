using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor_Effect : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<SetDatatoPlayer>().CharacterID1.Value != 2 && collision.gameObject.GetComponent<SetDatatoPlayer>().CharacterID2.Value != 2)
        {
            collision.gameObject.GetComponent<PlayerMovements>().Onstunned();
        }
    }
}
