using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;
public class CountDown : MonoBehaviour
{
    public TMP_Text countDownText;
 
    void Start()
    {
        StartCoroutine(countDown());
    }
    IEnumerator countDown()
    {
        int count = 3;
        while (count > 0)
        {
            countDownText.text = count.ToString();
            yield return new WaitForSeconds(1f);
            count -= 1;
        }
        Time.timeScale = 1;
        countDownText.text = "Hunt!!!!";
        yield return new WaitForSeconds(2f);
        countDownText.text = "";

    }
}
