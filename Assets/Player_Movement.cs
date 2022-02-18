using System.Collections;
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
    
    //bool isJumping;

    private PlayerCooltimeManager cooltimeManager;
    public GameObject attAngle;
    public bool isRightFace;
    private Animation roll;

    [SerializeField]
    private SpriteRenderer[] things;

    [SerializeField]
    private SliderManager slide;


    private void Awake()
    {
        state = GetComponent<PlayerTag>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cooltimeManager = GetComponent<PlayerCooltimeManager>();

        moveVelocity = Vector3.left;
        slide.hp.maxValue = base.HP;
        slide.hp.value = base.HP;

    }

    void Start()
    {


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
            

    }

    private void FixedUpdate()
    {
        LookChange();

        if (isGrounded)
        {
            //anim.SetBool("Jump", false);
            jumpCounter = 1;
            state.RemoveFlag(State._Jump);
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
        transform.position += moveVelocity * movePower * Time.deltaTime;

    }
    private void Jump()
    {
        if (!state.HasFlag(State._Jump))
            return;
        rigid.velocity = Vector2.zero;
        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
    }

    private IEnumerator Estus()
    {

        if (base.estusHPCurruntCount != 0)
        {
            base.estusHPCurruntCount -= 1;
            anim.Play("Estus");
            movePower = guardMovePower;

            slide.ResourceManager(0, +50f);

            yield return new WaitForSeconds(2f);
            state.RemoveFlag(State._Estus);
            movePower += guardMovePower;
        }

        else
            yield return null;



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
        if (Input.GetAxisRaw("Horizontal") != 0 && !playerFreeze)
        {
            state.AddFlag(State._Move);
            anim.SetFloat("idle", 0);
            Walk();
        }

        else if(Input.GetAxisRaw("Horizontal") == 0)
        {
            state.RemoveFlag(State._Move);
            anim.SetFloat("Move", 0);
            anim.SetFloat("idle", 1);
        }

        // 구르기
        if (Input.GetKeyDown(KeyCode.Mouse1) && cooltimeManager.canUseSkill[0] & !state.HasFlag(State._Jump))
        {
            foreach(SpriteRenderer sprite in things)
            {
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
            }

            state.AddFlag(State._Dodge);
            cooltimeManager.UseSkill(0);
            playerFreeze = true;
            Physics2D.IgnoreLayerCollision(15, 16, true);


            if (moveVelocity.x * transform.localScale.x == -1)      RollStateSetUp(-1);
            else if (moveVelocity.x * transform.localScale.x == 1)  RollStateSetUp(1);

            anim.SetTrigger("Roll");


            rigid.velocity = new Vector2(moveVelocity.x * dodgePower, 0f);
            //rigid.AddForce(moveVelocity * dodgePower, ForceMode2D.Impulse);
            StartCoroutine(ExitRoll(0.5f));

        }

        //가드 해제
        if (Input.GetKeyUp(KeyCode.K))
        {
            state.RemoveFlag(State._Guard);
            base.movePower += base.guardMovePower;
        }

        else if(Input.GetKey(KeyCode.K))
        {
            anim.Play("Guard");
            if(!state.HasFlag(State._Guard))            base.movePower -= base.guardMovePower;
            state.AddFlag(State._Guard);
        }



         //짬푸
         if (Input.GetButtonDown("Jump") && jumpCounter > 0 && isGrounded )
         {
             state.AddFlag(State._Jump);
             jumpCounter = 0;
             Jump();

         }


        //에스트
        if (Input.GetKeyDown(KeyCode.R))        StartCoroutine(Estus());

        if (Input.GetKeyDown(KeyCode.L))

        {
            slide.ResourceManager(0, -50f);
        }
        /*

         //패링
         if (Input.GetKey(KeyCode.Q)&& cooltimeManager.canUseSkill[1])
         {
             state.AddFlag(State._Parry);


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


        yield return new WaitForSecondsRealtime(time);

        state.RemoveFlag(State._Dodge);
        Physics2D.IgnoreLayerCollision(15, 16, false);
        playerFreeze = false;
        rigid.velocity = Vector2.zero;
        RollStateSetUp(0);

        foreach (SpriteRenderer sprite in things)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 255);
        }
    }



    public void StateChanger(bool value, State con)
    {
        if (value)
            state.AddFlag(con);
        else
            state.RemoveFlag(con);
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
