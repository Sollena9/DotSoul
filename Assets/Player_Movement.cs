using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Player_Movement : PlayerInfo
{
   public enum State
    {
        Idle, Walk, Jump, Attack, Guard, Guard_Walk, Estus, Estus_Walk, Dodge, Parry, Grab , Die

    };


    public State state;
    public Transform pos;
    public Vector2 boxSize;

    public bool playerFreeze;

    public Rigidbody2D rigid;
    public Animator anim;
    public Vector3 moveVelocity = Vector3.zero;
    
    bool isJumping;

    private PlayerCooltimeManager cooltimeManager;


    private void Awake()
    {   
        state = State.Idle;

    }

    void Start()
    {
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
        if(HP <= 0 && !(state == State.Die))
        {
            FindObjectOfType<SystemManager>().StartCoroutine("PrintYouDied");

            state = State.Die;
            //anim.SetBool("Die", true);
        }

        isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.1f, transform.position.y),
            new Vector2(transform.position.x + 0.1f, transform.position.y ), whatIsGrounded);               //두줄 이어지는 코드
            InputManager();


    }

    private void FixedUpdate()
    {
        if (isGrounded)
        {
            anim.SetBool("Jump", false);
            jumpcounter = 1;
        }


    }

    private void Walk()
    {

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;
            //transform.localScale = new Vector3(-1, 1, 1);
            anim.SetInteger("MoveSpeed", -1);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;
           // transform.localScale = new Vector3(1, 1, 1);
            anim.SetInteger("MoveSpeed", 1);

        }



       // rigid.AddForce(moveVelocity * movePower * Time.fixedDeltaTime);
        transform.position += moveVelocity * movePower * Time.deltaTime;

    }
    private void Jump()
    {
        if (!isJumping)
            return;
        StartCoroutine(BacktoIdle(State.Jump, 1f));
        anim.SetBool("Jump", true);
        rigid.velocity = Vector2.zero;
        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
        isJumping = false;
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



    private void Attack()
    {
        anim.SetTrigger("Attack");
        StartCoroutine(BacktoIdle(State.Attack, 0.4f));

        Collider2D[] col = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in col)
        {

            Debug.Log(collider.tag);
            //공격
            if (collider.gameObject.CompareTag("Enemy") && collider.GetComponent<EnemyInfo>().enemyState != EnemyInfo.State.Groggy)
            {
                //BacktoIdle(State.Attack, 1f);
                //var enemyHp = collider.GetComponent<EnemyHp>();
                //enemyHp.gameobject.setActive(true);
                //enemyHp.hpvar.value -= attackpower;
            }
            //잡기
            else if (collider.gameObject.tag == "Enemy" && collider.GetComponent<EnemyInfo>().enemyState == EnemyInfo.State.Groggy)
            {
                BacktoIdle(State.Grab, 10f);

                if (collider.GetComponent<EnemyInfo>().enemyLevel > 0)
                    anim.SetInteger("Grab", 1);
                else
                    anim.SetInteger("Grab", 0);

            }

        }



    }

    private IEnumerator BacktoIdle(State type, float time)
    {
        if(type == State.Dodge)
        {
            yield return new WaitForSecondsRealtime(0.5f);

            playerFreeze = false;
            rigid.velocity = new Vector2(0, 0);
            yield return new WaitForSecondsRealtime(0.3f);

            state = State.Idle;
        }

        else if(type == State.Attack)
        {
            yield return new WaitForSecondsRealtime(time);
            state = State.Idle;
        }

        else if(type == State.Jump)
        {
            yield return new WaitForSecondsRealtime(time);
            state = State.Idle;
        }
        else
        {
            state = State.Idle;
        }
    }


    bool StateManager(State type)
    {

        if (type == State.Attack && (state != State.Attack && state != State.Parry && state != State.Estus && state != State.Estus_Walk && state != State.Dodge))
        {
            state = type;
            return true;
        }

        else if (type == State.Parry && (state != State.Jump && state != State.Estus && state != State.Estus_Walk && state != State.Attack))
        {
            state = type;
            return true;
        }

        else if (type == State.Jump && (state != State.Estus && state != State.Estus_Walk && state != State.Guard_Walk && state != State.Guard))
        {
            state = type;
            return true;
        }

        else if (type == State.Dodge && (state != State.Jump && state != State.Estus && state != State.Estus_Walk))
        {
            state = type;
            return true;
        }

        else if (type == State.Walk && (state != State.Jump))
        {
            state = type;
            return true;
        }

        else
            state = State.Idle;
            return false;

        

    }


    void InputManager()
    {

        if(state == State.Die)
        {
            return;
        }

        //걷기
        if (Input.GetAxisRaw("Horizontal") != 0 && !playerFreeze && StateManager(State.Walk))
        {
            movePower = 10;
            Walk();
        }

        else if(Input.GetAxisRaw("Horizontal") == 0)
        {
            anim.SetInteger("MoveSpeed", 0);
        }

        if(Input.GetButtonUp("Horizontal"))
        {
            StartCoroutine(BacktoIdle(State.Walk, 0f));

        }


        //짬푸
        if (Input.GetButtonDown("Jump") && jumpcounter > 0 && isGrounded )
        {
            isJumping = true;
            StateManager(State.Jump);
            jumpcounter = 0;
            Jump();

        }


        //패링
        if (Input.GetKey(KeyCode.Q) && StateManager(State.Parry) && cooltimeManager.canUseSkill[1])
        {
            Collider2D col = Physics2D.OverlapBox(pos.position, boxSize, 0);
            if (col.gameObject.tag == "Enemy")
            {
                cooltimeManager.UseSkill(1);
                Debug.Log("체크");
            }

        }


        //공격
        // 잡기 상태가 StateManager에서 혼돈와서 나중에 수정해야됨
        if (Input.GetKeyDown(KeyCode.K) && StateManager(State.Attack))
            Attack();
       

        // 구르기
        if (Input.GetKeyDown(KeyCode.Z) && StateManager(State.Dodge) && cooltimeManager.canUseSkill[0])
        {
            cooltimeManager.UseSkill(0);
            playerFreeze = true;
            Physics2D.IgnoreLayerCollision(15, 16, true);

            anim.SetTrigger("Roll");
            rigid.AddForce(moveVelocity * dodgePower, ForceMode2D.Impulse);

            StartCoroutine(BacktoIdle(State.Dodge, 0.5f));
        }

        //가드/가드하고 걷기
        if (Input.GetKeyUp(KeyCode.S))
        {
            anim.SetBool("Guard", false);
        }
        else if (Input.GetKeyDown(KeyCode.S) && state == State.Walk)
        {
            StateManager(State.Guard_Walk);
            Guard_Walk();
        }

        else if (Input.GetKeyDown(KeyCode.S) && state == State.Idle)
        {
            StateManager(State.Guard);
            Guard();
        }


        //에스트


        if (Input.GetKeyDown(KeyCode.R) && state != State.Idle)
        {
            StateManager(State.Estus_Walk);
            Estus_Walk();
            StartCoroutine(StateChange(0.5f));
        }

        else if (Input.GetKeyDown(KeyCode.R) && state != State.Walk)
        {
            StateManager(State.Estus);
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

    private void ExitRoll()
    {
        Physics2D.IgnoreLayerCollision(15, 16, false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }

}
