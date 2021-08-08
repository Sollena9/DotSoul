using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;


namespace RotaryHeart.Lib {
public class Gundyr : MonoBehaviour
    {

        public string enemyName;
        public int enemyNum;
        public int phase;

        public int activeNum;
        public int[] attkDamage = new int[3];
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

            if(phase == 1)
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
                           // anim.SetBool("SwordInHeart", false);
                        }


                       if(playerDistance >= jumpDashRange)
                        {
                            anim.SetBool("JumpDashAttack", true);
                            //포물선 공식
                        }*/
                if (playerDistance <= attackRange && canUseSkill && enemyInfo.enemyState != EnemyInfo.State.Skill)
                {
                    canUseSkill = false;
                    enemyInfo.enemyState = EnemyInfo.State.Skill;
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
            if(enemyInfo.enemyState != EnemyInfo.State.Skill && enemyInfo.enemyState != EnemyInfo.State.Attack)
                enemyInfo.enemyState = EnemyInfo.State.Follow;
            
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
                        enemyInfo.enemyState = EnemyInfo.State.Idle;
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
                            enemyInfo.enemyState = EnemyInfo.State.Idle;
                            break;
                        }

                    else
                        attackCount++;
                    break;

                default:
                    attackCount = 1;
                    anim.SetInteger("Attack", 0);
                    enemyInfo.enemyState = EnemyInfo.State.Idle;
                    break;

            }

            

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