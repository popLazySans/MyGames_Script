using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    public GameObject page1;

    public void Page1()
    {
        page1.SetActive(true);
    }

    public void Page2()
    {
        page1.SetActive(false);
    }
}
