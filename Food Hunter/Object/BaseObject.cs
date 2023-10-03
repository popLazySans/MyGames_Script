using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    private string name = "";
    public int point;

   

    public BaseObject(string Name,int Point)
    {
        name = Name;
        point = Point;
    }

   
   
}
