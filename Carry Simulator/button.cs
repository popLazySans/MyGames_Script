using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class button : MonoBehaviour
{
    public Text WeightInput;
    public void Click()
    {
        carry.Weight = float.Parse(WeightInput.text);
    }
}
