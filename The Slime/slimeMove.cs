using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class slimeMove : MonoBehaviour
{
    public float jumpForce = 300f;
    public float speed =  100f;

    public bool jumpState = false;
    public bool groundState = false;
    public bool runState = false;

    AudioSource soundEffect;
    Rigidbody2D rigidbody;
    Animator anim;

    public bool leftCommand = false;
    public bool rightCommand = false;

    private PlayerControl_InputSystem playerControls;
    private float jumpInput;
    private float sideInput;

    public GameObject leftController;
    public GameObject rightController;
    public GameObject upController;
    private void Awake()
    {
        playerControls = new PlayerControl_InputSystem();
       
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        soundEffect = GetComponent<AudioSource>();
        falseController();
    }

    // Update is called once per frame
    void Update()
    {
      

        if (/*Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.UpArrow)*/jumpInput>0)
        {
            Jump();
        }

        if (/*Input.GetButtonDown("Horizontal")||(leftCommand == true || rightCommand == true)*/sideInput != 0)
        {
                runState = true;
           
           // anim.SetTrigger("run");
        }
        if(/*Input.GetButtonUp("Horizontal")||(leftCommand == false && rightCommand == false)*/ sideInput == 0)
        {
            runState = false;
            
        }
        
        anim.SetBool("afk", groundState);
        anim.SetBool("run", runState);
        if(runState == false&&groundState == true)
        {
            anim.SetTrigger("rest");
        }
    }
    public void Jump()
    {
        if (groundState == true)
        {
            jumpState = true;
            groundState = false;
            anim.SetTrigger("jump");
            soundEffect.Play();
        }
    }
    public void LeftMove()
    {
        gameObject.transform.position += Vector3.left * Time.deltaTime * speed;
        gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    public void RightMove()
    {
        gameObject.transform.position += Vector3.right * Time.deltaTime * speed;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public void SwipeToJump(float isJump)
    {
        jumpInput = isJump;
    }
    public void SwipeToWalk(float isWalk)
    {
        sideInput = isWalk;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        groundState = true;
    }
    public void FixedUpdate()
    {
        //jumpInput = playerControls.PlayerControl.Jump.ReadValue<float>();
        //sideInput = playerControls.PlayerControl.Move.ReadValue<float>();
        if (runState == true)
        {
            if (/*Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow)||leftCommand == true*/sideInput<0)
            {
                LeftMove();
                if (groundState == true)
                {
                    LeftController();
                }
                
                
            }
            else if (/*Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow)|| rightCommand == true*/sideInput>0)
            {
                RightMove();
                if (groundState == true)
                {
                    RightController();
                }
            }
            
        }
        
        if (jumpState == true)
        {
            rigidbody.AddForce(new Vector2(0f,jumpForce));
            jumpInput = 0;
            UpController();
            jumpState = false;

        }
    }
    public void falseController()
    {
        leftController.SetActive(false);
        rightController.SetActive(false);
        upController.SetActive(false);
    }
    public void LeftController()
    {
        leftController.SetActive(true);
        rightController.SetActive(false);
        upController.SetActive(false);
    }
    public void RightController()
    {
        leftController.SetActive(false);
        rightController.SetActive(true);
        upController.SetActive(false);
    }
    public void UpController()
    {
        leftController.SetActive(false);
        rightController.SetActive(false);
        upController.SetActive(true);
    }
}
