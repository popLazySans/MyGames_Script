using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class TextLookAtPlayer : NetworkBehaviour
{
    public GameObject playerWithName;
    private Vector3 playerPos;
    private GameObject[] playerList;
    // Start is called before the first frame update
    void Start()
    {
        playerList = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner) return;
        foreach (GameObject player in playerList)
        {
            if (player != playerWithName)
            {
                playerPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            }
        }
        gameObject.transform.LookAt(playerPos);
    }
}
