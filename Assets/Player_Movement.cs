using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using SpriteTrail;

public class Player_Movement : PlayerInfo
{

    public bool playerFreeze;

    public Vector3 moveVelocity = Vector3.zero;

    
    //bool isJumping;
    public GameObject attAngle;
    public bool isRightFace;

    [SerializeField]
    private SpriteRenderer[] things;


    [SerializeField]
    private SpriteTrail.SpriteTrail trail;



    private void Awake()
    {

        moveVelocity = Vector3.left;

    }

   

    void Update()
    {
        InputManager();

        if (HP <= 0)
        {
            FindObjectOfType<SystemManager>().StartCoroutine("PrintYouDied");

            //anim.SetBool("Die", true);
        }

        isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.1f, transform.position.y),
            new Vector2(transform.position.x + 0.1f, transform.position.y ), whatIsGrounded);               //두줄 이어지는 코드

        //if (!state.HasFlag(State._Combat)) slide.;

    }

    private void FixedUpdate()
    {
        LookChange();

        if (isGrounded)
        {
            //anim.SetBool("Jump", false);
            jumpCounter = 1;
        }



    }

    private void Walk()
    {

        if (Input.GetAxisRaw("Horizontal") < 1)
        {
            moveVelocity = Vector3.left;
            anim.SetFloat("Move", 1);
        }
        else if (Input.GetAxisRaw("Horizontal") > -1)
        {
            moveVelocity = Vector3.right;
            anim.SetFloat("Move", 1);

        }



       // rigid.AddForce(moveVelocity * movePower * Time.fixedDeltaTime);
        transform.position += moveVelocity * MoveSpeed * Time.deltaTime;

    }
    private void Jump()
    {

        if (data.playerState != PlayerData.playerCombatState.Jump)
            return;
        rb2d.velocity = Vector2.zero;
        Vector2 jumpVelocity = new Vector2(0, data.jumpPower);
        rb2d.AddForce(jumpVelocity, ForceMode2D.Impulse);
    }

    private IEnumerator Estus(int value)
    {
        if (data.playerState != PlayerData.playerCombatState.Estus)
            yield return null;


        switch(value)
        {
            case 0:
                if (data.estusHPCurruntCount != 0)
                {
                    data.estusHPCurruntCount -= 1;
                    anim.Play("Estus");
                    MoveSpeed_Buff -= data.moveSpeed_Guard;
                    slide.ResourceManager(0, +50f);

                    yield return new WaitForSeconds(2f);
                    data.playerState = PlayerData.playerCombatState.Idle;
                    MoveSpeed_Buff += data.moveSpeed_Guard;
                }
                break;
            case 1:
                
                break;
            case 2:
                break;
        }
        




    }

    

    /*
    public void ReturnToidle(string param, float num)
    {
        Debug.Log("실행");
        anim.SetFloat(param, num);
    }

    */


    void InputManager()
    {

        //걷기
        if (Input.GetAxisRaw("Horizontal") != 0 && data.playerState != PlayerData.playerCombatState.Freeze)
        {
            data.playerState = PlayerData.playerCombatState.Walk;
            anim.SetFloat("idle", 0);
            Walk();
        }

        else if(Input.GetAxisRaw("Horizontal") == 0)
        {
            data.playerState = PlayerData.playerCombatState.Idle;
            anim.SetFloat("Move", 0);
            anim.SetFloat("idle", 1);
        }

        // 구르기
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (cooltimeManager.canUseSkill[0] && data.playerState != PlayerData.playerCombatState.Jump && slide.sp.value > 0)
                StartCoroutine(ExitRoll(0.5f));
            else
                return;

        }

        //가드 해제
        if (Input.GetKeyUp(KeyCode.K))
        {
            if (data.playerState == PlayerData.playerCombatState.Guard)
                MoveSpeed_Buff += data.moveSpeed_Guard;
            data.playerState = PlayerData.playerCombatState.Idle;
            anim.SetBool("Guard", false);
        }

        else if(Input.GetKey(KeyCode.K))
        {
            anim.SetBool("Guard", true);
            if(data.playerState != PlayerData.playerCombatState.Guard)
                MoveSpeed_Buff -= data.moveSpeed_Guard;
            data.playerState = PlayerData.playerCombatState.Guard;
            Debug.Log(data.moveSpeed);
            //가드 시 방어력 추가 해줘야 될까??

        }



         //짬푸
         if (Input.GetButtonDown("Jump") && jumpCounter > 0 && isGrounded )
         {
            data.playerState = PlayerData.playerCombatState.Jump;
            jumpCounter = 0;
            Jump();

         }


        //에스트
        if (Input.GetKeyDown(KeyCode.R))        StartCoroutine(Estus(0));

        if (Input.GetKeyDown(KeyCode.L))        slide.ResourceManager(0, -50f);
        /*

         //패링
         if (Input.GetKey(KeyCode.Q))//&& cooltimeManager.canUseSkill[1])
         {
             //state.AddFlag(State._Parry);
            anim.StopPlayback();


         }


        */
 
       

    }

    /*
    IEnumerator StateChange(float time)
    {

        yield return new WaitForSeconds(time);
        anim.SetBool("Estus_Walk", false);
        anim.SetBool("Estus", false);

    }
    */
    private void LookChange()
    {
        //Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (playerFreeze)
            return;

        else if (Input.mousePosition.x <= 960)
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

    IEnumerator ExitRoll(float time)
    {
        //slide.ResourceManager(2, -100f);

        foreach (SpriteRenderer sprite in things)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
        }

        data.playerState = PlayerData.playerCombatState.Roll;
        cooltimeManager.UseSkill(0);
        playerFreeze = true;
        Physics2D.IgnoreLayerCollision(15, 16, true);


        if (moveVelocity.x * transform.localScale.x == -1) RollStateSetUp(-1);
        else if (moveVelocity.x * transform.localScale.x == 1) RollStateSetUp(1);

        anim.SetTrigger("Roll");


        rb2d.velocity = new Vector2(moveVelocity.x * data.rollPower, 0f);
        //rigid.AddForce(moveVelocity * dodgePower, ForceMode2D.Impulse);


        trail.EnableTrail();
        //foreach (SpriteTrail.SpriteTrail arry in trails)
        //{
        //    arry.EnableTrail();
        //}


        yield return new WaitForSecondsRealtime(time);

        data.playerState = PlayerData.playerCombatState.Idle;
        Physics2D.IgnoreLayerCollision(15, 16, false);
        playerFreeze = false;
        rb2d.velocity = Vector2.zero;
        RollStateSetUp(0);

        foreach (SpriteRenderer sprite in things)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 255);
        }

        trail.DisableTrail();

        //foreach (SpriteTrail.SpriteTrail arry in trails)
        //{
        //    arry.DisableTrail();
        //}

    }




    private void RollStateSetUp(int vector)
    {
        if (vector == 0)
        { 
            anim.SetFloat("RollDirection_Left", 0);
            anim.SetFloat("RollDirection_Right", 0);
        }
        else if(vector == 1)    anim.SetFloat("RollDirection_Right", 1);
        else if (vector == -1)  anim.SetFloat("RollDirection_Left", 1);

    }



}
