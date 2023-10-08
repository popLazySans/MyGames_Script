using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemList : MonoBehaviour
{
    public List<GameObject> player1_Item;
    public List<GameObject> player2_Item;
    public Image item1, item2;

    // Start is called before the first frame update
    void Start()
    {
        item1.GetComponent<Image>();
        item2.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player1_Item[0] != null)
        {
            
            changeImage_Item1();
          
        }
        else
        {
            changeImage_Blank2();
        }
        if (player2_Item[0] != null)
        {
         
            changeImage_Item2();
            
        }
        else
        {
            changeImage_Blank2();
        }
       
        /*if (player1_Item.Count > 0 )
        {
            // Player 1 item UI

            item1.sprite = player1_Item[0].gameObject.GetComponent<SpriteRenderer>().sprite;

        }
        if (player2_Item.Count > 0)
        {
            // Player 2 item UI

            item2.sprite = player2_Item[0].gameObject.GetComponent<SpriteRenderer>().sprite;
        }*/
    }

    public void changeImage_Item1()
    {
        item1.sprite = player1_Item[0].gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public void changeImage_Item2()
    {
        item2.sprite = player2_Item[0].gameObject.GetComponent<SpriteRenderer>().sprite;
    }
    public void changeImage_Blank1()
    {
        item1.sprite = null;
    }
    public void changeImage_Blank2()
    {
        item2.sprite = null;
    }
}
