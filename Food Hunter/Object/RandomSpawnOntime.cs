using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class RandomSpawnOntime : NetworkBehaviour
{
    public int StartTime;
    public int OnTime;
    public int freq;
    private int multipleFreq;
    private CharactorPoint charactorPoint;
    private bool isSpawned;
    public GameObject prefabSpawn;
    public List<GameObject> spawnedList = new List<GameObject>();
    private RandomObjectSpawner randomObjectSpawner;
  
    // Start is called before the first frame update
    void Start()
    {
        if (!IsLocalPlayer || !IsOwnedByServer) return;
        charactorPoint = gameObject.GetComponent<CharactorPoint>();
        randomObjectSpawner = gameObject.GetComponent<RandomObjectSpawner>();
        multipleFreq = freq;
        StartTime = charactorPoint.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsLocalPlayer || !IsOwnedByServer) return;
        OnTime = charactorPoint.time;
        if (StartTime-multipleFreq == OnTime && isSpawned == false)
        {
            SpawnTreasure();
        }
        else
        {
            isSpawned = false;
        }
    }

    public void SpawnTreasure()
    {
        int random_X = randomObjectSpawner.randomPosition(randomObjectSpawner.x_Range);
        int random_Z = randomObjectSpawner.randomPosition(randomObjectSpawner.z_Range);
        GameObject prefabtoSpawn = Instantiate(prefabSpawn, new Vector3(random_X, 0.45f, random_Z),Quaternion.Euler(-90,0,0));
        spawnedList.Add(prefabtoSpawn);
        prefabtoSpawn.GetComponent<DisappearObject>().spawnOntime = this;
        prefabtoSpawn.GetComponent<NetworkObject>().Spawn(true);
        multipleFreq = multipleFreq + freq;
        isSpawned = true;
    }
   
    [ServerRpc]
    public void DestroyTreasureServerRpc(ulong networkObjectId)
    {
        GameObject toDestroy = findObjectfromNetworkId(networkObjectId);
        if (toDestroy == null) return;
        toDestroy.GetComponent<NetworkObject>().Despawn();
        spawnedList.Remove(toDestroy);
        toDestroy.SetActive(false);
    }
    public GameObject findObjectfromNetworkId(ulong networkObjectId)
    {
        foreach (GameObject gameObject in spawnedList)
        {
            ulong objId = gameObject.GetComponent<NetworkObject>().NetworkObjectId;
            if (objId == networkObjectId)
            {
                return gameObject;
            }
        }
        return null;
    }
}
