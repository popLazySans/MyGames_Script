using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class fadeManager : MonoBehaviour
{
    public GameObject fadeObj;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = fadeObj.GetComponent<Animator>();
        anim.SetTrigger("fadeIn");
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    public void fadeOutToAnotherScene()
    {
        anim.SetTrigger("fadeOut");
    }
}
