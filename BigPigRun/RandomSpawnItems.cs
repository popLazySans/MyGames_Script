using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnItems : MonoBehaviour
{
    public int Amount;
    ItemList itemList;
    // Start is called before the first frame update
    void Start()
    {
        itemList = gameObject.GetComponent<ItemList>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsItemAvailable())
        {
            SpawnItems();
        }
    }
    public bool IsItemAvailable()
    {
        if (FindItemInGame() == null)
        {
            return false;
        }
        return true;
    }
    public GameObject FindItemInGame()
    {
        GameObject item = GameObject.FindGameObjectWithTag("Item");
        if (item == null)
        {
            return null;
        }
        return item;
    }
    public void SpawnItems()
    {
        for (int i=0;i<Amount;i++)
        {
            int randomObjectType = Random.Range(0,itemList.Items.Count);
            float randomSpawnNumberX = Random.Range(-25+gameObject.transform.position.x,25+gameObject.transform.position.x);
            float randomSpawnNumberY = Random.Range(-25+gameObject.transform.position.z, 25+gameObject.transform.position.z);
            Spawn(randomObjectType,randomSpawnNumberX,randomSpawnNumberY);
        }
    }
    public void Spawn(int Item_number,float position_numberX,float positon_numberY)
    {
        Instantiate(itemList.Items[Item_number],new Vector3(position_numberX, 1.59f, positon_numberY),Quaternion.identity);
    }
}
