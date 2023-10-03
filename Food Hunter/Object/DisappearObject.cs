using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
public class DisappearObject : NetworkBehaviour
{
    public RandomObjectSpawner spawner;
    public RandomSpawnOntime spawnOntime;
    private TMP_Text treasureText;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "Treasure")
        {
            treasureText = GameObject.Find("TreasureText").GetComponent<TMP_Text>();
            StartCoroutine(showText());
        }
    }
    IEnumerator showText()
    {
        treasureText.text = "Treasure has come!!!";
        yield return new WaitForSeconds(2f);
        treasureText.text = "";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            DestroyServerRpc();
            //ulong networkObjectId = GetComponent<NetworkObject>().NetworkObjectId;
            //Debug.Log(networkObjectId.ToString());
            //spawner.DestroyNetworkObjectServerRpc(networkObjectId);
            gameObject.SetActive(false);
        }
    }
    [ServerRpc(RequireOwnership =false)]
    public void DestroyServerRpc()
    {
        if (gameObject.tag == "Treasure")
        {
            spawnOntime.DestroyTreasureServerRpc(gameObject.GetComponent<NetworkObject>().NetworkObjectId);
        }
        else
        {
             gameObject.GetComponent<NetworkObject>().Despawn();
        }
    }
}
