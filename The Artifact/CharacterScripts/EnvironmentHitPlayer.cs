using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnvironmentHitPlayer : MonoBehaviour
{
    public float knockBackDistant = -5.0f;
    public GameObject objectSprite;
    SpriteRenderer sprite;
    public ObjectMove moveBack;
    public characterControl characterControl;
    public GameObject WLcontroller;
    public AudioSource hitSound;

    private int hp_Character=10;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
       
        characterControl = gameObject.GetComponent<characterControl>();
        sprite = objectSprite.GetComponent<SpriteRenderer>();
        moveBack = gameObject.GetComponent<ObjectMove>();
        moveBack.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = hp_Character;
        if (hp_Character <= 0)
        {
            WLcontroller.GetComponent<WinOrLose>().Lose(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x + 
                knockBackDistant, gameObject.transform.position.y);
            if (knockBackDistant != 0)
            {
                hitSound.Play();
                hp_Character -= 1;
                iFrame();

            }
        }
        if (collision.CompareTag("BangFai")||collision.CompareTag("Banana"))
        {
            if (knockBackDistant != 0)
            {
                hitSound.Play();
                hp_Character -= 2;
                Stun();
                iFrame();
                
            }
            else
            {

            }
        }
        if (characterControl.normalform.sprite == characterControl.tuktukform && collision.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);
        }
        if (collision.CompareTag("Artifact"))
        {
            collision.gameObject.SetActive(false);
            WLcontroller.GetComponent<WinOrLose>().Win(gameObject);
        }
    }
    public void iFrame()
    {
        knockBackDistant = 0;
        InvokeRepeating("iFrameAnimation",0,0.5f);
        Invoke("timeOutiFrame", 3f);
    }
    public void iFrameAnimation()
    {
        hideSprite();
        Invoke("showSprite", 0.25f);
    }
    void hideSprite()
    {
        sprite.enabled = false;
    }
    void showSprite()
    {
        sprite.enabled = true;
    }
    void timeOutiFrame()
    {
        CancelInvoke("iFrameAnimation");
        knockBackDistant = -5f;
    }
    void Stun()
    {
        characterControl.idle();
        moveBack.enabled = true;
        Invoke("timeOutStun", 3f);
    }
    void timeOutStun()
    {
        characterControl.idle();
      moveBack.enabled = false ;
    }
}
