using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ShowBookData : MonoBehaviour
{
    public List<GameObject> foodGameObject = new List<GameObject>();
    private int page = 1;
    public TMP_Text leftPageText;
    public TMP_Text rightPageText;
    public Image leftPageImage;
    public Image rightPageImage;
    private ShowText showText;
    private ItemDetail itemDetailLeft;
    private ItemDetail itemDetailRight;
    void Start()
    {
        showText = GameObject.FindGameObjectWithTag("ShowText").GetComponent<ShowText>();
        
    }
    void Update()
    {
        if (foodGameObject[page-1] != null)
        {
            itemDetailLeft = foodGameObject[page-1].GetComponent<ItemDetail>();
            showText.ShowFoodDetail(leftPageText, itemDetailLeft.name, itemDetailLeft.detail, itemDetailLeft.energy, itemDetailLeft.fat, itemDetailLeft.vitamin);
            leftPageImage.sprite = itemDetailLeft.image2d;
        }
        else
        {
            leftPageText.text = "";
        }
        if (foodGameObject[page] != null)
        {
            itemDetailRight = foodGameObject[page].GetComponent<ItemDetail>();
            showText.ShowFoodDetail(rightPageText, itemDetailRight.name, itemDetailRight.detail, itemDetailRight.energy, itemDetailRight.fat, itemDetailRight.vitamin);
            rightPageImage.sprite = itemDetailRight.image2d;
        }
        else
        {
            rightPageText.text = "";
        }
    }
    public void LeftPageButton()
    {
        if (page > 2)
        {
            page -= 2;
        }
    }
    public void RightPageButton()
    {
        if (page < foodGameObject.Count-1)
        {
            page += 2;
        }
    }
}
