using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    private List <GameObject> gameObjectTypePoollist = new List <GameObject>();
    private List<GameObject> gameObjectsPoollist = new List<GameObject>();
    private const int MAX_OBJECT_AMOUNT = 60;
    
    public void AddGameObjectTypeToPool( GameObject PoolingObject)
    {
        gameObjectTypePoollist.Add(PoolingObject);
        gameObjectTypePoollist[gameObjectTypePoollist.Count-1].name = "Object Type : " + gameObjectTypePoollist.Count;
       Debug.Log("Added :"+ gameObjectTypePoollist.Count);
    }
    public void CreateRandomlyGameObjectFromPool()
    {
        for (int i =0; i<MAX_OBJECT_AMOUNT;i++)
        {
            int RandomType = Random.Range(0, gameObjectTypePoollist.Count);
            CreateGameObject(RandomType,i);
            Debug.Log("Added : " + gameObjectsPoollist[i].name + " Type : " + (RandomType+1));
        }
    }
    public void CreateGameObject(int type,int number)
    {
        gameObjectsPoollist.Add(gameObjectTypePoollist[type]);
        gameObjectsPoollist[number].name = "Object : " + number.ToString();
        gameObjectsPoollist[number].SetActive(true);
        gameObjectsPoollist[number] = Instantiate(gameObjectsPoollist[number],gameObjectsPoollist[number].transform.position.normalized, gameObjectsPoollist[number].transform.rotation.normalized);
    }
 
    public GameObject EnableObjectInPool(int NumberObject)
    {
        gameObjectsPoollist[NumberObject].SetActive(true);
        return gameObjectsPoollist[NumberObject];
    }

    public void DisableObjectInPool(GameObject DisableGameObject)
    {
        for (int i = 0; i < MAX_OBJECT_AMOUNT; i++)
        {
            if (gameObjectsPoollist[i].name == DisableGameObject.name)
            {
                gameObjectsPoollist[i].SetActive(false);
                break;
            }
        }
    }
}
