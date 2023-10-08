using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class characterControl : MonoBehaviour
{
    public int jumpCount = 0;
    private bool isSlide = false;
    private Rigidbody2D rigidbody;
    public int jumpForce = 500;
    private Animator anim;
    public GameObject anim_Object;
    private ItemList itemList;
    private Collider2D collider;
    private float StateValue;
    private float usingItemValue;

    public GameObject itemBangFai, itemTukTuk,itemBanana;
    public GameObject spawnPoint_Front;
    public GameObject spawnPoint_Back;

    public AudioSource jumpSound, slideSound;

    public bool OnGround;
    private bool isRun;
    
    private EnvironmentHitPlayer environment;
    public BoxCollider2D collider2D;

    public SpriteRenderer normalform;
    public Sprite tuktukform;

    public GameObject blank;

    private PickUpScript pickupscript;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        
        environment = GetComponent<EnvironmentHitPlayer>();
        anim =anim_Object.GetComponent<Animator>();
        collider2D = GetComponent<BoxCollider2D>();
        normalform = anim_Object.GetComponent<SpriteRenderer>();
        itemList = GameObject.FindGameObjectWithTag("ItemList").GetComponent<ItemList>();
        pickupscript = gameObject.GetComponent<PickUpScript>();
        anim.SetTrigger("isIdle");
    }

    // Update is called once per frame
    void Update()
    {
        if (StateValue <= 1 && StateValue>0 && OnGround == true && gameObject.CompareTag("Player1"))
        {
            jump();
        }
        if (StateValue <-0.5 &&StateValue >= -1 && OnGround ==true && gameObject.CompareTag("Player1"))
        {
            slide();
        }
        if (usingItemValue != 0 &&  gameObject.CompareTag("Player1"))
        {
            useItem1();
        }

        if (StateValue <= 1 && StateValue > 0 && OnGround == true && gameObject.CompareTag("Player2"))
        {
            jump();
        }
        if (StateValue < 0 && StateValue >= -1 && OnGround == true && gameObject.CompareTag("Player2"))
        {
            slide();
        }
        if (usingItemValue != 0  && gameObject.CompareTag("Player2"))
        {
            useItem2();
        }
        if (StateValue >-0.5 && StateValue <=0 && OnGround == true)
        {
            Run();
        }

        anim.SetBool("isRun", isRun);
        anim.SetBool("OnGround", OnGround);
    }
    public void UpAndDownControl(InputAction.CallbackContext context)
    {
        StateValue = context.ReadValue<float>();
    }
    public void UsingItemControl(InputAction.CallbackContext context)
    {
        usingItemValue = context.ReadValue<float>();
    }
     void Run() { 
        jumpCount = 0;
        isRun = true;
        collider2D.size = new Vector2(0.35f, 1.35f);
        collider2D.offset = new Vector2(-0.2f, 0f);
       

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
            OnGround = true;
            Run(); 
    }

    void jump()
    {
        jumpSound.Play();
        isRun = false;
        anim.SetTrigger("Jump");
        rigidbody.AddForce(new Vector2(0f, jumpForce));
        OnGround = false;
    }

    void slide()
    {
        slideSound.Play();
        isRun = false;
        isSlide = true;
        anim.SetTrigger("isSlide");
        collider2D.size = new Vector2(1.35f,0.35f);
        collider2D.offset = new Vector2(-0.2f, -0.15f);
    }
    public void idle()
    {
        isRun = !isRun;
        anim.SetTrigger("isIdle");
    }

    void useItem1()
    {
        if(itemList.player1_Item[0].name == "BangFai"){ DeleteItemList1();UseBangfai();}
        else if (itemList.player1_Item[0].name == "Tuktuk"){ DeleteItemList1(); UseTuktuk();}
        else if (itemList.player1_Item[0].name == "banana"){ DeleteItemList1(); UseBanana();}
        else{Debug.Log("Error");}
    }  
    void useItem2()
    {
        if (itemList.player2_Item[0].name == "BangFai"){ DeleteItemList2(); UseBangfai();}
        else if (itemList.player2_Item[0].name == "Tuktuk"){ DeleteItemList2(); UseTuktuk();}
        else if (itemList.player2_Item[0].name == "banana"){ DeleteItemList2(); UseBanana();}
        else { Debug.Log("Error"); }
    }
    private void UseBanana()
    {
        Instantiate(itemBanana, spawnPoint_Back.transform.position, itemBanana.transform.rotation);
    }

    private void UseTuktuk()
    {
        
        anim.enabled = false;
        environment.knockBackDistant = 0;
        TranformToTukTuk();
        Invoke("enableEnvironment", 5.0f);
    }

    private void UseBangfai()
    {
        Instantiate(itemBangFai, spawnPoint_Front.transform.position, itemBangFai.transform.rotation);
    }
    private void DeleteItemList1()
    {
        pickupscript.itemCount = false;
        itemList.player1_Item[0] = blank;
    }
    private void DeleteItemList2()
    {
        pickupscript.itemCount = false;
        itemList.player2_Item[0] = blank;
    }
  

    void enableEnvironment()
    {
        environment.iFrame();
        anim.enabled = true;
    }
    void TranformToTukTuk()
    {
        normalform.sprite = tuktukform;
    }
   
    
}
