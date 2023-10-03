using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
public class StaminaBar : MonoBehaviour
{
    private Slider staminaSlider;
    public List<GameObject> inSliderChild = new List<GameObject>();
    Animator anim;
    public bool isAppered;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        staminaSlider = GetComponent<Slider>();
  
    }
    // Update is called once per frame
    void Update()
    {
        if (staminaSlider.maxValue == staminaSlider.value)
        {
            isAppered = false;
        }
        else
        {
            isAppered = true;
        }
        anim.SetBool("fadeOut",!isAppered);
        anim.SetBool("fadeIn", isAppered);
    }
    public void ChangColor(bool isAppeared)
    {
        foreach (GameObject SliderChild in inSliderChild)
        {
            Image sliderChildImage = SliderChild.GetComponent<Image>();
            var tempColor = sliderChildImage.color;
            if (isAppeared == false)
            {
                tempColor.a = 0f;
            }
            else
            {
                tempColor.a = 255f;
            }
        }
    }
 
}
