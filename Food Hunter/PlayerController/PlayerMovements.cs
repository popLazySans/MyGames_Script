using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;
using Unity.Netcode.Components;
public class PlayerMovements : NetworkBehaviour
{
    Rigidbody Player_Rigibody;
    public float Player_MoveSpeed;
    public float Player_RotateSpeed;
    public float Player_JumpHeight;
    // Start is called before the first frame update
    private float ForOrBack;
    private float LeftOrRight;
    private bool RunOrWalk;

    public Transform Transform_Forward;
    public Transform Transform_Backward;
    public Transform Transform_Left;
    public Transform Transform_Right;
    public Transform Transform_ForLeft;
    public Transform Transform_ForRight;
    public Transform Transform_BackLeft;
    public Transform Transform_BackRight;

    public List<Animator> Player_Anim = new List<Animator>();
    public bool isIdle = true;
    public bool isWalk = false;
    public bool isRun = false;
    public bool isJump = false;
    public bool groundState = true;
    public bool isOverheat = false;
    public List<GameObject> modelAnim = new List<GameObject>();
    public List <OwnerNerworkAnimator> ownerNerworkAnimator = new List<OwnerNerworkAnimator>();
    private StaminaValue staminaValue;
    private SetDatatoPlayer setDatatoPlayer;
    public bool isStunned = false;
    void Start()
    {
        //setDatatoPlayer = gameObject.GetComponent<SetDatatoPlayer>();
        //if (IsLocalPlayer)
        //{
        //    ownerNerworkAnimator = setDatatoPlayer.characterinThisObjectList[setDatatoPlayer.CharacterID1.Value].GetComponent<OwnerNerworkAnimator>();
        //    Player_Anim = setDatatoPlayer.characterinThisObjectList[setDatatoPlayer.CharacterID1.Value].GetComponent<Animator>();
        //}
        //else
        //{
        //    ownerNerworkAnimator = setDatatoPlayer.characterinThisObjectList[setDatatoPlayer.CharacterID2.Value].GetComponent<OwnerNerworkAnimator>();
        //    Player_Anim = setDatatoPlayer.characterinThisObjectList[setDatatoPlayer.CharacterID2.Value].GetComponent<Animator>();
        //}
        for(int i =0;i<modelAnim.Count;i++)
        {
            Player_Anim[i] = modelAnim[i].GetComponent<Animator>();
            ownerNerworkAnimator[i] = modelAnim[i].GetComponent<OwnerNerworkAnimator>();
        }
        Player_Rigibody = GetComponent<Rigidbody>();
        staminaValue = GetComponent<StaminaValue>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
    
        Rotation_Controller();
            if (ForOrBack != 0 || LeftOrRight != 0)
            {
                if (RunOrWalk == true)
                {
                    OnRun();
                }
                else
                {
                    OnWalk();
                }
            }
            else
            {
                if (!IsOwner || isStunned) return;
                OnIdle();
            } 
        
        if(groundState == false)
        {
            OnJump();
        }
        SetAnim();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            groundState = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            groundState = false;
        }
    }
    public void Forward_Backward_move(InputAction.CallbackContext context)
    {
        if (!IsOwner || isStunned) return;
        ForOrBack = context.ReadValue<float>();
    }
    public void Left_Right_move(InputAction.CallbackContext context)
    {
        if (!IsOwner || isStunned) return;
        LeftOrRight = context.ReadValue<float>();
    }
    public void Run_Move(InputAction.CallbackContext context)
    {
        if (!IsOwner || isStunned) return;
        Debug.Log(context.ReadValue<float>());
        if (context.ReadValue<float>() == 0f)
        {
            isOverheat = false;
        }
        if (context.ReadValue<float>() > 0 && isOverheat == false)
        {
            RunOrWalk = true;
        }
        else { RunOrWalk =  false; }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (!IsOwner || isStunned) return;
        if (context.ReadValue<float>() > 0 && groundState == true)
        {
           Player_Rigibody.AddForce(0, Player_JumpHeight, 0);
        }
    }
    public void OnWalk()
    {
        if (isStunned) return;
        if (staminaValue.staminaCanRecover())
        {
            staminaValue.staminaRecovery();
        }
        Debug.Log("Is Walking!!");
        //Player_Rigibody.velocity = transform.forward * Player_MoveSpeed*Time.deltaTime;
        transform.Translate(Vector3.forward * Player_MoveSpeed * Time.deltaTime);
        AnimController("walk");
    }
    public void OnIdle()
    {
        if (staminaValue.staminaCanRecover())
        {
            staminaValue.staminaRecovery();
        }
        //Player_Rigibody.velocity = transform.forward*Time.deltaTime;
        AnimController("idle");
    }
    public void OnRun()
    {
        if (isStunned) return;
        if (staminaValue.staminaCanDrop()&& isOverheat == false)
        {
            staminaValue.staminaDrop();
            Debug.Log("Is Running!!");
            //Player_Rigibody.velocity = transform.forward * Player_MoveSpeed* 5 * Time.deltaTime;
            if (gameObject.GetComponent<SetDatatoPlayer>().CharacterID1.Value == 1 || gameObject.GetComponent<SetDatatoPlayer>().CharacterID2.Value == 1)
            {
                transform.Translate(Vector3.forward * Player_MoveSpeed * 2.5f * 1.25f * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.forward * Player_MoveSpeed * 2.5f * Time.deltaTime);
            }
            AnimController("run");
        }
        else
        {
            isOverheat = true;
            OnWalk();
        }
    }
    public void OnJump()
    {
        AnimController("jump");
    }
    public void Onstunned()
    {
        AnimController("stun");
        StartCoroutine(delayStun());
    }
    IEnumerator delayStun()
    {
        yield return new WaitForSeconds(4);
        isStunned = false;
    }
    public void CanRecoveryStamina()
    {
        if (staminaValue.stamina < staminaValue.stamina_Max)
        {
            staminaValue.staminaRecovery();
        }
    }
    public void AnimController(string word)
    {
        if (word == "idle" && groundState == true)
        {
            isIdle = true;
            foreach(OwnerNerworkAnimator ownerNerwork in ownerNerworkAnimator){
                ownerNerwork.SetTrigger("Stand");
            }
            
        }
        else { isIdle = false; }
        if (word == "walk" && groundState == true)
        {
            isWalk = true;
            foreach (OwnerNerworkAnimator ownerNerwork in ownerNerworkAnimator)
            {
                ownerNerwork.SetTrigger("Walk");
            }
        }
        else { isWalk = false;}
        if (word == "run" && groundState == true)
        {
            isRun = true;
            foreach (OwnerNerworkAnimator ownerNerwork in ownerNerworkAnimator)
            {
                ownerNerwork.SetTrigger("Run");
            }
        }
        else { isRun = false; }
        if (word == "jump")
        {
            isJump = true;
            foreach (OwnerNerworkAnimator ownerNerwork in ownerNerworkAnimator)
            {
                ownerNerwork.SetTrigger("Jump");
            }
        }
        else { isJump = false; }
        if (word == "stun")
        {
            isStunned = true;
            foreach (Animator animator in Player_Anim)
            {
                animator.SetTrigger("Stun");
            }
            foreach (OwnerNerworkAnimator ownerNerwork in ownerNerworkAnimator)
            {
                ownerNerwork.SetTrigger("Stun");
            }
        }
        else { isStunned = false; }
    }
    public void SetAnim()
    {
        foreach(Animator animator in Player_Anim)
        {
        animator.SetBool("Stand",isIdle);
        animator.SetBool("Walk", isWalk);
        animator.SetBool("Run", isRun);
        animator.SetBool("Jump", isJump);
        }
       
    }
    public void Rotation_Controller()
    {
        if (ForOrBack == 1 && LeftOrRight == 0)
        {
            Forward_Rotate();
        }
        else if (ForOrBack == -1 && LeftOrRight == 0)
        {
            Backward_Rotate();
        }
        else if (ForOrBack == 0 && LeftOrRight == -1)
        {
            Left_Rotate();
        }
        else if (ForOrBack == 0 && LeftOrRight == 1)
        {
            Right_Rotate();
        }
        else if (ForOrBack == 1 && LeftOrRight == -1)
        {
            ForwardAndLeft_Rotate();
        }
        else if (ForOrBack == 1 && LeftOrRight == 1)
        {
            ForwardAndRight_Rotate();
        }
        else if (ForOrBack == -1 && LeftOrRight == -1)
        {
            BackwardAndLeft_Rotate();
        }
        else if (ForOrBack == -1 && LeftOrRight == 1)
        {
            BackwardAndRight_Rotate();
        }
    }

    public void Forward_Rotate()
    {
        Debug.Log("To Forward");
        transform.rotation = Quaternion.Slerp(transform.rotation,Transform_Forward.rotation,Player_RotateSpeed*Time.deltaTime);
    }
    public void Backward_Rotate()
    {
        Debug.Log("To Backward");
        transform.rotation = Quaternion.Slerp(transform.rotation, Transform_Backward.rotation, Player_RotateSpeed * Time.deltaTime);
    }
    public void Left_Rotate()
    {
        Debug.Log("To Left");
        transform.rotation = Quaternion.Slerp(transform.rotation, Transform_Left.rotation, Player_RotateSpeed * Time.deltaTime);
    }
    public void Right_Rotate()
    {
        Debug.Log("To Right");
        transform.rotation = Quaternion.Slerp(transform.rotation, Transform_Right.rotation, Player_RotateSpeed * Time.deltaTime);
    }
    public void ForwardAndLeft_Rotate()
    {
        Debug.Log("To Forward and Left");
        transform.rotation = Quaternion.Slerp(transform.rotation, Transform_ForLeft.rotation, Player_RotateSpeed * Time.deltaTime);
    }
    public void ForwardAndRight_Rotate()
    {
        Debug.Log("To Forward and Right");
        transform.rotation = Quaternion.Slerp(transform.rotation, Transform_ForRight.rotation, Player_RotateSpeed * Time.deltaTime);
    }
    public void BackwardAndLeft_Rotate()
    {
        Debug.Log("To Backward and Left");
        transform.rotation = Quaternion.Slerp(transform.rotation, Transform_BackLeft.rotation, Player_RotateSpeed * Time.deltaTime);
    }
    public void BackwardAndRight_Rotate()
    {
        Debug.Log("To Backward and Right");
        transform.rotation = Quaternion.Slerp(transform.rotation, Transform_BackRight.rotation, Player_RotateSpeed * Time.deltaTime);
    }
}
