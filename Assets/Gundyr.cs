using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using RotaryHeart.Lib;
using DG.Tweening;

public class Gundyr : EnemyInfo
{

    public string enemyName;
    public int enemyNum;
    public int phase;

    public int activeNum;
    public float[] attkDamage = new float[3];
    public int enemyHp;
    public float moveSpeed;

    //public float sightRange;
    public float attackRange;
    [SerializeField]
    private int dropSouls;

    public float attackCoolTime;
    private float curruntCoolTime;
    private bool canUseSkill = true;
    public int attackCount = 1;
    private int attackNum;
    [SerializeField]
    private GundyrSkill gundyrSkillScript;



    public Transform attackPos;
    public Vector2 attackSize;
    public float playerDistance;

    public bool firstTry;
    public bool swordInHeart;
    public bool death;
    private bool isFacingRight;

    private Vector3[] waypoints = new Vector3[2];
    public AnimationCurve ease;
    public int playerLayer, enemyLayer;

    private EnemyInfo enemyInfo;
    private Animator anim;
    private SpriteRenderer sp;
    private Player_Movement player;


    // Start is called before the first frame update
    void Start()
    {

        enemyInfo = GetComponent<EnemyInfo>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<Player_Movement>();


        ChangeState(EnemyStates.idle);


        //StartCoroutine(enemyStateManager());

        if(firstTry)
        {
            anim.SetBool("", true);
        }

        if(phase == 1)
        {
            StartCoroutine(PhaseSelector());
        }


        JumpAttack();



    }


    private void FixedUpdate()
    {
            //Debug.Log(enemyState);

        if (!death)
        {
            anim.SetInteger("Walk", activeNum);
            Move();
        }

        if ((activeNum > 0 && isFacingRight == false) || (activeNum < 0 && isFacingRight == true))
        {
            Flip();
        }
        


    }




    public void Move()
    {
        Vector3 activeVelocity = new Vector3(activeNum, 0, 0);
        transform.position += activeVelocity * moveSpeed * Time.deltaTime;
    }
       
    IEnumerator enemyStateManager()
    {
        playerDistance = Vector3.Distance(transform.position, player.transform.position);
        /*
            1순위: 잠수에서 깨어날때
            2순위: 점프공격
            3순위: 공격
            4순위: 추격
            5순위: 아이들 -> 사실상 없음s
        */

        /*        if (swordInHeart)
                {
                    yield return new WaitForSeconds(2f);
                    // anim.SetBool("SwordInHeart", falsse);
                }


                if(playerDistance >= jumpDashRange)
                {
                    anim.SetBool("JumpDashAttack", true);
                    //포물선 공식
                }*/
        if (playerDistance <= attackRange && canUseSkill && curruntStates != states[3]) // 스킬이 아니면 
        {
            canUseSkill = false;
            ChangeState(EnemyStates.Skill);
            StartCoroutine(AttackCoolTimeCalc());
            Attack();

            Debug.Log("공격실행");
        }

   
        {

            if (player.transform.position.x >= transform.position.x)
            {
                activeNum = 1;
            }
            else
                activeNum = -1;
        }

    yield return new WaitForSeconds(1f);
    StartCoroutine(enemyStateManager());
        if (curruntStates != states[3] && curruntStates != states[2]) // 스킬과 어택이 아니면 
            ChangeState(EnemyStates.idle); // follow로 바꿔야됨
            
    }

    IEnumerator PhaseSelector()
    {
        if(enemyHp <= 1037)
        {
            phase = 2;
        }

        yield return new WaitForSeconds(1f);
    }

    private void Attack()
    {
        if (attackCount == 1)
            attackNum = Random.Range(1, 11);

        Debug.Log("attacknum: " + attackNum + "/ attackCount: " + attackCount);

        switch(attackCount)
        {
            case 0:
                break;

            case 1:
                anim.SetInteger("Attack", gundyrSkillScript.skillArr[attackNum].skillOrder[0]);
//                Debug.Log(gundyrSkillScript.skillArr[attackNum].skillOrder[0]);   
                if (gundyrSkillScript.skillArr[attackNum].skillOrder[0] == 0)
                    attackCount = 1;
                else
                    attackCount++;

                break;

            case 2:
                anim.SetInteger("Attack", gundyrSkillScript.skillArr[attackNum].skillOrder[1]);
                    //              Debug.Log(gundyrSkillScript.skillArr[attackNum].skillOrder[1]);

                if (gundyrSkillScript.skillArr[attackNum].skillOrder[1] == 0)
                {
                    attackCount = 1;
                    anim.SetInteger("Attack", 0);
                    ChangeState(EnemyStates.idle);

                    break;
                }
                else
                    attackCount++;
                break;

            case 3:
                anim.SetInteger("Attack", gundyrSkillScript.skillArr[attackNum].skillOrder[2]);
    //              Debug.Log(gundyrSkillScript.skillArr[attackNum].skillOrder[2]);
      
                if (gundyrSkillScript.skillArr[attackNum].skillOrder[2] == 0)
                    {
                        attackCount = 1;
                        anim.SetInteger("Attack", 0);
                    ChangeState(EnemyStates.idle);

                    break;
                    }

                else
                    attackCount++;
                break;

            default:
                attackCount = 1;
                anim.SetInteger("Attack", 0);
                ChangeState(EnemyStates.idle);
                break;

        }

            

    }


    void JumpAttack()
    {
        SetJumpPosition();


        //transform.DOJump(player.transform.position, 5f, 1, 1f, false);
        // 위치, 속도, 점프 횟수, 부드러운 이동

        Physics2D.IgnoreLayerCollision(15, 16, true);

        transform.DOMove(waypoints[0], 0.9f).SetEase(Ease.OutQuint).
            OnStepComplete(() =>
            {
                transform.DOMove(waypoints[1], 0.4f).SetEase(Ease.OutQuint).OnStepComplete(() =>
                {
                    transform.DOPunchPosition(new Vector3(0.3f, 0, 0), 0.7f, 30, 1f);
                    Physics2D.IgnoreLayerCollision(15, 16, false);
                });

            });

        //transform.DOPath(waypoints, 1.5f, PathType.CatmullRom, gizmoColor: Color.green).SetEase(ease).
        //    OnStepComplete(() =>
        //    {
        //        transform.DOPunchPosition(new Vector3(0.5f, 0, 0), 1f, 30, 1f);
        //        Physics2D.IgnoreLayerCollision(15, 16, false);
        //    });



    }

    private void SetJumpPosition()
    {

        Vector3 pos = new Vector3(player.transform.position.x / 5 * 4, 6f, 0f);
        waypoints[0] = pos;
        // + 상황
        if (!isFacingRight)
            waypoints[0].x *= -1;
        waypoints[1] = new Vector2(player.transform.position.x, player.transform.position.y);



    }

    IEnumerator AttackCoolTimeCalc()
        {
            curruntCoolTime = attackCoolTime;
            while (curruntCoolTime > 0)
            {
                curruntCoolTime -= 1 * Time.smoothDeltaTime;

                //Debug.Log(curruntCoolTime);
                yield return null;
            }
            canUseSkill = true;
            Debug.Log("스킬 사용 가능");
            yield break;
        }

        IEnumerator PTD()
        {
            yield return new WaitForSeconds(0.3f);
            gameObject.SetActive(false);
        }

        private void Flip()
        {
            isFacingRight = !isFacingRight;
            Vector2 enemyScale = transform.localScale;
            enemyScale.x *= -1;
            transform.localScale = enemyScale;

        }


        private void ParryingChecker()
        {
            if(true)//player.state == Player_Movement.State.Parry)패링
                {
                    anim.SetBool("Groggy", true);
                    ChangeState(EnemyStates.Groggy);
                    Debug.Log("Parry Success");
                    //behaviorNum = 4;  
                    //StopCoroutine(enemyStateManager());

                }
        }

        public void DamageToPlayer()
        {
            Collider2D col = Physics2D.OverlapBox(attackPos.position, attackSize, 0);
            
            //공격
            if (col.gameObject.CompareTag("Player"))    
            {
                SliderManager slide = FindObjectOfType<SliderManager>();
                slide.ResourceManager(1, attkDamage[attackCount -1]);
            }
            
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(attackPos.position, attackSize);
        }

        
}