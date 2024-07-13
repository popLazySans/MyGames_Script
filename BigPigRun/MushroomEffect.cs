using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomEffect : MonoBehaviour
{
    private ItemDetail itemDetail;
    // Start is called before the first frame update
    void Awake()
    {
        itemDetail = gameObject.GetComponent<ItemDetail>();
    }
    private void OnEnable()
    {
        itemDetail.energy = Random.Range(0,1000);
        itemDetail.fat = Random.Range(0, 100);
        itemDetail.vitamin = Random.Range(0, 100);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
