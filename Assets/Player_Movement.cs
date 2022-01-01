﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


public class Player_Movement : PlayerInfo
{



    public PlayerTag state;
    public bool playerFreeze;

    public Rigidbody2D rigid;
    public Animator anim;
    public Vector3 moveVelocity = Vector3.zero;
    
    bool isJumping;

    private PlayerCooltimeManager cooltimeManager;
    public GameObject attAngle;
    public bool isRightFace;

    private void Awake()
    {

    }

    void Start()
    {
        state = GetComponent<PlayerTag>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cooltimeManager = GetComponent<PlayerCooltimeManager>();
        isJumping = false;
        moveVelocity = Vector3.left;

        var slide = FindObjectOfType<SliderManager>();
        slide.hp.maxValue = base.HP;
        slide.hp.value = base.HP;

    }


    void Update()
    {
        if(HP <= 0)
        {
            FindObjectOfType<SystemManager>().StartCoroutine("PrintYouDied");

            //anim.SetBool("Die", true);
        }

        isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.1f, transform.position.y),
            new Vector2(transform.position.x + 0.1f, transform.position.y ), whatIsGrounded);               //두줄 이어지는 코드
            InputManager();
        LookChange();

    }

    private void FixedUpdate()
    {


        if (isGrounded)
        {
            //anim.SetBool("Jump", false);
            jumpcounter = 1;
        }


    }

    private void Walk()
    {

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;
            anim.SetInteger("MoveSpeed", -1);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;
            anim.SetInteger("MoveSpeed", 1);

        }



       // rigid.AddForce(moveVelocity * movePower * Time.fixedDeltaTime);
        transform.position += moveVelocity * movePower * Time.deltaTime;

    }
    private void Jump()
    {
        if (!state.HasFlag(State._Jump))
            return;
        anim.SetBool("Jump", true);
        rigid.velocity = Vector2.zero;
        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
        state.RemoveFlag(State._Jump);
    }

    private void Guard()
    {
        anim.SetBool("Guard", true);

        DP = 100;

    }

    private void Guard_Walk()
    {
        anim.SetBool("Guard_Walk", true);

        DP = 100;
        movePower = guardMovePower;
    }

    private void Estus()
    {
        anim.SetBool("Estus", true);
        movePower = guardMovePower;
        HP += 50;
    }

    private void Estus_Walk()
    {
        anim.SetBool("Estus_Walk", true);

        movePower = guardMovePower;
        HP += 50;
    }







    void InputManager()
    {

        //걷기
        if (Input.GetAxisRaw("Horizontal") != 0 && !playerFreeze)
        {
            movePower = 10;
            state.AddFlag(State._Move);
            Walk();
        }

        else if(Input.GetAxisRaw("Horizontal") == 0)
        {
            state.RemoveFlag(State._Move);
            //anim.SetInteger("MoveSpeed", 0);
        }

       

        //짬푸
        if (Input.GetButtonDown("Jump") && jumpcounter > 0 && isGrounded )
        {
            state.AddFlag(State._Jump);
            jumpcounter = 0;
            Jump();

        }


        //패링
        if (Input.GetKey(KeyCode.Q)&& cooltimeManager.canUseSkill[1])
        {
            state.AddFlag(State._Parry);


        }


        //공격
        // 잡기 상태가 StateManager에서 혼돈와서 나중에 수정해야됨
        if (Input.GetKeyDown(KeyCode.Mouse0))
        { 
            state.AddFlag(State._Attack);
            state.AddFlag(State._Combat);
            anim.SetTrigger("Attack");
            
        }

        // 구르기
        if (Input.GetKeyDown(KeyCode.Z) && cooltimeManager.canUseSkill[0])
        {
            state.AddFlag(State._Dodge);
            cooltimeManager.UseSkill(0);
            playerFreeze = true;
            Physics2D.IgnoreLayerCollision(15, 16, true);

            anim.SetTrigger("Roll");
            rigid.AddForce(moveVelocity * dodgePower, ForceMode2D.Impulse);

        }

        //가드/가드하고 걷기
        if (Input.GetKeyUp(KeyCode.S))
        {
            anim.SetBool("Guard", false);
        }
        else if (Input.GetKeyDown(KeyCode.S))
   
            Guard_Walk();

        else if (Input.GetKeyDown(KeyCode.S))
        {
            Guard();
        }


        //에스트


        if (Input.GetKeyDown(KeyCode.R))
        {
            state.AddFlag(State._Estus);
            Estus_Walk();
            StartCoroutine(StateChange(0.5f));
        }

        else if (Input.GetKeyDown(KeyCode.R))
        {

            Estus();
            StartCoroutine(StateChange(0.5f));
        }

    }


    IEnumerator StateChange(float time)
    {

        yield return new WaitForSeconds(time);
        anim.SetBool("Estus_Walk", false);
        anim.SetBool("Estus", false);

    }

    private void LookChange()
    {
        //Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.mousePosition.x <= 960)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            isRightFace = false;
        }

        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            isRightFace = true;
        }
       
    }


    private void ExitRoll()
    {
        Physics2D.IgnoreLayerCollision(15, 16, false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(pos.position, boxSize);
    }

    public void asdfasdfasdfasdfasdfasdfasdf(bool value, State con)
    {
        if (value)
            state.AddFlag(con);
        else
            state.RemoveFlag(con);
    }


}
