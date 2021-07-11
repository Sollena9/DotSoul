using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClericHollow : MonoBehaviour
{
    // Start is called before the first frame update

    public string enemyName;
    public int enemyNum;
    public int behaviorNum;
    // 0: wait, 1:move, 2: chase, 3: attack

    public int activeNum;
    public int attkDamage;
    public int enemyHp;
    public float moveSpeed;

    public float sightRange;
    public float attackRange;
    public float attackCooltime;
    public float attackCooltimeSave;
    public int attackKind;
    public Transform attackPos;
    public Vector2 attackSize;
    public float playerDistance;

    public bool death;
    private bool isFacingRight;

    private Animator anim;
    private SpriteRenderer sp;
    private PlayerInfo player;
    private EnemyInfo enemyinfo;

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerInfo>();
        enemyinfo = GetComponent<EnemyInfo>();

        StartCoroutine(ActiveSelector());
        StartCoroutine(EnemyStateManager());
        StartCoroutine(Turn());

    }

    // Update is called once per frame
    void Update()
    {

        if (!death)
        {
            anim.SetInteger("Walk", activeNum);
            Move();
        }

        if (behaviorNum != 3 && attackCooltime > 0)
        {
            attackCooltime -= Time.deltaTime;
        }
        else if (attackCooltime < 0)
            attackCooltime = 0;

        // 공격 쿨타임 연산



    }


    private void FixedUpdate()
    {
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

    IEnumerator Turn()
    {
        Vector2 frontVec = new Vector2(transform.position.x + activeNum * 0.5f, transform.position.y - 0.1f);
        //적의 진행방향을 기준으로 앞의 벡터 값 정의(와 말 간지난다 지렷다)
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
        //Raycast 함수를 호출해서 frontVec 값을 대입한 뒤 해당 좌표에 Ground Layer를 가진 객체가 있는지 확인한 뒤 bool 값을 반환함

        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        // raycast로 낭떠러지 계산하는 line 출력

        if (!rayHit.collider)
        //만약 raycast로 호출한 rayHit 값이 true라면 
        {
            activeNum *= -1;
            //적의 이동 vector 값 중 x값을 반전시킨다
        }

        yield return new WaitForSecondsRealtime(1f);
        StartCoroutine(Turn());

    }

    IEnumerator EnemyStateManager()
    {
        playerDistance = Vector3.Distance(transform.position, player.transform.position);


        if (playerDistance <= attackRange && attackCooltime <= 0 && behaviorNum != 3)
        {
            Attack();

        }

        else if (playerDistance <= sightRange && behaviorNum != 3)
        {
            StopCoroutine(ActiveSelector());
            behaviorNum = 2;
            if (player.transform.position.x > transform.position.x)
            {
                activeNum = 1;
            }
            else
                activeNum = -1;
        }

        else if (playerDistance >= sightRange && behaviorNum >= 2)
        {
            behaviorNum = 0;
            StartCoroutine(ActiveSelector());
        }

        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemyStateManager());

    }

    private void Attack()
    {
        StopCoroutine(ActiveSelector());
        activeNum = 0;
        behaviorNum = 3;

        attackKind = Random.Range(1, 3);
        anim.SetInteger("Attack", attackKind);
        attackCooltime = attackCooltimeSave;

        Collider2D[] col = Physics2D.OverlapBoxAll(attackPos.position, attackSize, 0);
        foreach (Collider2D collider in col)
        {

            Debug.Log(collider.tag);

            if (collider.gameObject.tag == "Player")
            {
                FindObjectOfType<SystemManager>().ChangeH_S_FP(0, -attkDamage);
                Debug.Log(collider.gameObject.tag);
            }

        }
    }

    private void ClearAttackKind()
    {
        attackKind = 0;
        anim.SetInteger("Attack", attackKind);
        behaviorNum = 0;
        StartCoroutine(ActiveSelector());
    }

    /*
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

    }*/


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



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, attackSize);
    }

}