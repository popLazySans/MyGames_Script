using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightAnimate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().enabled = false;
        StartCoroutine(openthelight());
    }

    // Update is called once per frame
    IEnumerator openthelight()
    {
        yield return new WaitForSeconds(Random.Range(0,3f));
        GetComponent<Animator>().enabled = true;
    }
}
