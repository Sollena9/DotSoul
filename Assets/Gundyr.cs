using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;


namespace RotaryHeart.Lib {
public class Gundyr : MonoBehaviour
    {

    public string enemyName;
    public int enemyNum;
    public int phaseLv;

    public int activeNum;
    public int[] attkDamage = new int[3];
    public int enemyHp;
    public float moveSpeed;

    //public float sightRange;
    public float attackRange;
    [SerializeField]
    private int dropSouls;


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

        enemyInfo.enemyState = EnemyInfo.State.Idle;


        //StartCoroutine(ActiveSelector());
        StartCoroutine(enemyStateManager());
        //StartCoroutine(Turn());

        if(firstTry)
        {
            anim.SetBool("", true);
        }

        if(phaseLv == 1)
        {
            StartCoroutine(PhaseSelector());
        }

        
    }


    private void FixedUpdate()
    {
            //Debug.Log(enemyInfo.enemyState);

        if (!death)
        {
            anim.SetInteger("Walk", activeNum);
            Move();
        }

        if ((activeNum > 0 && isFacingRight == false) || (activeNum < 0 && isFacingRight == true))
        {
            Flip();
        }
        
        //CoolTimeCalculator();


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
             5순위: 아이들 -> 사실상 없음
            */

    /*        if (swordInHeart)
            {
                yield return new WaitForSeconds(2f);
               // anim.SetBool("SwordInHeart", false);
            }


           if(playerDistance >= jumpDashRange)
            {
                anim.SetBool("JumpDashAttack", true);
                //포물선 공식
            }*/

            if(playerDistance <= attackRange)
            {
                Attack();
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
        if(enemyInfo.enemyState != EnemyInfo.State.Skill && enemyInfo.enemyState != EnemyInfo.State.Attack)
            enemyInfo.enemyState = EnemyInfo.State.Follow;
            
        }

    IEnumerator PhaseSelector()
    {
        if(enemyHp <= 1037)
        {
            phaseLv = 2;
        }

        yield return new WaitForSeconds(1f);
    }

    private void Attack()
    {
        enemyInfo.enemyState = EnemyInfo.State.Skill;
        if (attackCount == 1)
        attackNum = Random.Range(1, 11);

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
                    attackCount = 1;
                else
                    attackCount++;
                break;

            case 3:
                anim.SetInteger("Attack", gundyrSkillScript.skillArr[attackNum].skillOrder[2]);
  //              Debug.Log(gundyrSkillScript.skillArr[attackNum].skillOrder[2]);

                if (gundyrSkillScript.skillArr[attackNum].skillOrder[2] == 0)
                    attackCount = 1;
                else
                    attackCount++;
                break;

            default:
                anim.SetInteger("Attack", 0);
                attackCount = 1;
                enemyInfo.enemyState = EnemyInfo.State.Idle;
                break;

        }

            

    }
    /*
    private void ClearAttackKind()
    {
        attackKind = 0;
        anim.SetInteger("Attack", attackKind);
        behaviorNum = 0;
        StartCoroutine(ActiveSelector());
    }

    IEnumerator FindPlayer()
    // 플레이어 추적
    {
        playerDistance = Vector3.Distance(transform.position, player.transform.position);
        Debug.Log(playerDistance);

        if (playerDistance <= sightRange)
        {
            StopCoroutine(ActiveSelector());
            behaviorNum = 1;
            if (player.transform.position.x > transform.position.x)
            {
                activeNum = 1;
            }
            else
                activeNum = -1;
        }

        else if (playerDistance > sightRange && behaviorNum >= 2)
        {
            behaviorNum = 0;
            StartCoroutine(ActiveSelector());
        }

        yield return new WaitForSeconds(1f);
        StartCoroutine(FindPlayer());

    }

        
    IEnumerator ActiveSelector()
    {
        if (behaviorNum <= 1)
        {
            activeNum = Random.Range(-1, 1);
            if (activeNum == 0)
                behaviorNum = 0;
            else
                behaviorNum = 1;

        }
        yield return new WaitForSeconds(5f);
        StartCoroutine(ActiveSelector());
    }*/

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

    /*private void CoolTimeCalculator()
    {
        for(int i = 0; i < 2; i++)
        { 
            if (attackCooltime[i] > 0)
            {
                attackCooltime[i] -= Time.deltaTime;
            }
            else if (attackCooltime[i] < 0)
                attackCooltime[i] = 0;
        }
    }
    */

    private void ParryingChecker()
    {
        if(player.state == Player_Movement.State.Parry)
            {
                anim.SetBool("Groggy", true);
                enemyInfo.enemyState = EnemyInfo.State.Groggy;
                Debug.Log("Parry Success");
                //behaviorNum = 4;  
                //StopCoroutine(enemyStateManager());

            }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, attackSize);
    }

    }
}