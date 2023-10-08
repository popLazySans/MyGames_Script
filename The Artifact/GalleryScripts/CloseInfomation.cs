using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseInfomation : MonoBehaviour
{
    public GameObject information;

    public void CloseInformationMenu()
    {
        information.SetActive(false);
    }
}
