using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternPooling : MonoBehaviour
{
    public List<GameObject> poolObjectList = new List<GameObject>();
    public PoolObject poolObject = new PoolObject();

    private List<GameObject> objectInTime = new List<GameObject>();
    private bool delaySpawn;
    void Start()
    {
        for (int i = 0;i<poolObjectList.Count;i++)
        {
            poolObject.AddGameObjectTypeToPool(poolObjectList[i]);
        }
        poolObject.CreateGameObjectFromPool();
    }


    void FixedUpdate()
    {
        bool isSpawned = poolObject.SpawnStatus();
        if (isSpawned == false && delaySpawn == false)
        {
            StartCoroutine(SpawnFrequency());
            delaySpawn = true;
        }
        else if (isSpawned == true)
        {
          // StartCoroutine(Stop());
        }
    }
    
    IEnumerator SpawnFrequency()
    {
        for (int i = 0;i<10;i++)
        {
            RandomObjectSpawner();
            yield return new WaitForSeconds(3f);
        }
       
    }
    IEnumerator Stop()
    {
        yield return new WaitForSeconds(30f);
        objectInTime[0].transform.position = gameObject.transform.position;
        poolObject.DisableObjectInPool(objectInTime[0]);
        objectInTime.RemoveAt(0);
        RandomObjectSpawner();
        
       
    }
    public void RandomObjectSpawner()
    {
        int randomNumber = Random.Range(0, poolObjectList.Count);
        if (poolObject.SpawnStatusSelection(randomNumber) == false)
        {
            objectInTime.Add(poolObject.EnableObjectInPool(randomNumber));
            StartCoroutine(Stop());
        }
        else
        {
            RandomObjectSpawner();   
        }
    }
}
