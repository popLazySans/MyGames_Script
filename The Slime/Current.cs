using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Current : MonoBehaviour
{
    public int HP;
    static public int Score = 0;
    //public Image heartUI;
    public TMP_Text textHealthBar;
    public TMP_Text ScoreBar;
    public bool ImmortalTime = false;
    SpriteRenderer slimesh;
    public GameObject reButt;
    public GameObject hmButt;
    public TMP_Text resultText;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        reButt.SetActive(false);
        resultText.SetText("");
        HP = 3;
        slimesh = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        textHealthBar.text = HP.ToString();
        ScoreBar.text = Score.ToString();
        if (Score == 0)
        {
            Win();
        }
        else if (HP == 0)
        {
            Lose();
        }
        if(AiScript.isAttack == true && ImmortalTime == false)
        {
            HP -= 1;
            AiScript.isAttack = false;
            StartCoroutine(delayDamage());
            StartCoroutine(ImmortalStage(0.1f));
            
        }
       
    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && ImmortalTime == false)
        {
            HP -= 1;
            StartCoroutine(delayDamage());
            StartCoroutine(ImmortalStage(0.1f));


        }
    }
    

    IEnumerator delayDamage()
    {
        ImmortalTime = true;
        yield return new WaitForSeconds(2);
        ImmortalTime = false;
    }
    IEnumerator ImmortalStage(float duration)
    {
        while (ImmortalTime == true) {
            slimesh.enabled = !slimesh.enabled;
            yield return new WaitForSeconds(duration);
        }
    }
    public void Win()
    {
        reButt.SetActive(true);
        hmButt.SetActive(true);
        resultText.SetText("Win");
        Time.timeScale = 0;
    }
    public void Lose()
    {
        reButt.SetActive(true);
        hmButt.SetActive(true);
        resultText.SetText("Lose");
        Time.timeScale = 0;
    }
}
