using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    private List <GameObject> gameObjectTypePoollist = new List <GameObject>();
    private List<GameObject> gameObjectsPoollist = new List<GameObject>();
    private const int MAX_OBJECT_AMOUNT = 12;
    
    public void AddGameObjectTypeToPool( GameObject PoolingObject)
    {
        gameObjectTypePoollist.Add(PoolingObject);
        gameObjectTypePoollist[gameObjectTypePoollist.Count-1].name = "Object Type : " + gameObjectTypePoollist.Count;
       Debug.Log("Added :"+ gameObjectTypePoollist.Count);
    }
    public void CreateGameObjectFromPool()
    {
        for (int i = 0;i<MAX_OBJECT_AMOUNT;i++)
        {
            CreateGameObject(i, i);
            Debug.Log("Added : " + gameObjectsPoollist[i].name + "Type : " + (i + 1));
        }
       
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
        gameObjectsPoollist[number].SetActive(false);
        gameObjectsPoollist[number] = Instantiate(gameObjectsPoollist[number], gameObject.transform.position, gameObject.transform.rotation);
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
    public bool SpawnStatus()
    {
        for(int i = 0;i<MAX_OBJECT_AMOUNT; i++)
        {
            if (gameObjectsPoollist[i].activeInHierarchy == true)
            {
                
            }
            else
            {
                return false;
            }
        }
        return true;
    }
    public bool SpawnStatusSelection(int numberObject)
    {

        if (gameObjectsPoollist[numberObject].activeInHierarchy == true)
        {
            return true;
        }

        else
        {
            return false;
        }

    }
}
