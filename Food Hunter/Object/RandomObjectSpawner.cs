using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class RandomObjectSpawner : NetworkBehaviour
{
    public PoolObject poolObject;
    public int x_Range = 0;
    public int z_Range = 0;
    public int amoutObject;
    Transform spawnerPos;
    public List<GameObject> gameObjectsTypeList = new List<GameObject>();
    private List<GameObject> gameObjectsList = new List<GameObject>();
    void Start()
    {
        if (IsOwnedByServer||IsOwner) return;
        spawnerPos = GameObject.Find("ObjectSpawner").GetComponent<Transform>();
        AddObjectTypeToPoolList(gameObjectsTypeList);
        poolObject.CreateRandomlyGameObjectFromPool();
        RandomSetObject();
   
    }


    void Update()
    {
        
    }
    public void AddObjectTypeToPoolList(List<GameObject> gameObjectList)
    {
        for (int i = 0;i<gameObjectList.Count;i++)
        {
            poolObject.AddGameObjectTypeToPool(gameObjectList[i]);
        }
    }

    public void RandomSetObject()
    {
        
        for (int i = 0;i<amoutObject;i++)
        {

            int random_X ;
            int random_Z ;

            while (true)
            {
                random_X = randomPosition(x_Range);
                random_Z = randomPosition(z_Range);
                if (gameObjectsList != null)
                {
                    bool isSamePosition = CheckObjectPosition(random_X, random_Z);
                    if (isSamePosition == true)
                    {

                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
              
            }

            SetObject(i,((int)spawnerPos.position.x) + random_X,((int)spawnerPos.position.z) + random_Z);
        }
    }
    public Vector3 GetObjectPosition()
    {
        return this.transform.position;
    }
    
    public void SetObject(int numberObject,int position_x,int position_z)
    {
        GameObject moving_Object = poolObject.EnableObjectInPool(numberObject);
        gameObjectsList.Add(moving_Object);
        moving_Object.transform.position = new Vector3(position_x, transform.position.y, position_z);
        moving_Object.GetComponent<DisappearObject>().spawner = this;
        moving_Object.GetComponent<NetworkObject>().Spawn(true);
    }
    [ServerRpc (RequireOwnership =false)]
    public void DestroyNetworkObjectServerRpc(ulong networkObjectId)
    {
        GameObject toDestroy = findGameObjectFromNetworkId(networkObjectId);
        if (toDestroy == null) return;
        toDestroy.GetComponent<NetworkObject>().Despawn();
        //gameObjectsList.Remove(toDestroy);
        toDestroy.SetActive(false);
    }
    private GameObject findGameObjectFromNetworkId(ulong networkObjectId)
    {
        foreach (GameObject obj in gameObjectsList)
        {
            ulong objId = obj.GetComponent<NetworkObject>().NetworkObjectId;
            if (objId == networkObjectId)
            {
                return obj;
            }
        }
        return null;
    }
    public bool CheckObjectPosition(int posX,int posZ)
    {
        bool isSamePosition = false;
        for (int i = 0;i<gameObjectsList.Count;i++)
        {
            if (posX == gameObjectsList[i].transform.position.x && posZ == gameObjectsList[i].transform.position.z)
            {
                isSamePosition = true;
            }
        }
        return isSamePosition;
    }
    public int randomPosition(int range)
    {
        int randomNumber = Random.Range(1, range);
        return randomNumber;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PoolingObject")
        {
            poolObject.DisableObjectInPool(collision.gameObject);
        }
        
    }
}
