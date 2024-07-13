using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    Teleporter doctorTeleport;
    public Transform toTranform;
    public string message;
    // Start is called before the first frame update
    void Start()
    {
        doctorTeleport = GameObject.FindGameObjectWithTag("Teleporter").GetComponent<Teleporter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            doctorTeleport.Teleport(toTranform);
            doctorTeleport.UpdateMessage(message);
        }
    }
}
