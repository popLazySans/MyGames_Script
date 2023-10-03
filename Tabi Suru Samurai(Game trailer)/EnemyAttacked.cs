using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacked : MonoBehaviour
{
    public int HP = 3;
    public GameObject particleGameObject;
    SpriteRenderer mesh;
    Animator anim;
    public string trigger;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HP == 0) 
        {
            anim.SetTrigger(trigger.ToString());
        }
    }
    public void OnMouseDown()
    {
        particleGameObject.SetActive(true);
        HP -= 1;
        StartCoroutine(CloseParticle());
        Debug.Log("Hit");
        if(HP == 0)
        {
            TobeContinueScene.defeated_count += 1;
        }
    }
    IEnumerator CloseParticle()
    {
        yield return new WaitForSeconds(1);
        particleGameObject.SetActive(false);
    }
}
