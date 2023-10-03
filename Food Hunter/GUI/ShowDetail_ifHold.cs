using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ShowDetail_ifHold : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    public GameObject detailObject;
    public void OnPointerEnter(PointerEventData eventData)
    {
        detailObject.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        detailObject.SetActive(false);
    }
}
