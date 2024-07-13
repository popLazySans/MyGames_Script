using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemDetail : MonoBehaviour
{
    public string name,detail;
    public int energy,fat,vitamin,vision;
    public Sprite image2d;
    public ItemDetail(string Name, string Detail, int Energy, int Fat, int Vitamin,int Vision,Sprite image)
    {
        name = Name;
        detail = Detail;
        energy = Energy;
        fat = Fat;
        vitamin = Vitamin;
        image2d = image;
        vision = Vision;
    }
}
