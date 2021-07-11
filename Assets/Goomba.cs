using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{

    public string enemyName;
    public int enemyNum;
    public int activeNum;
    public int attkDamage;
    public int enemyHp;
    public float moveSpeed;
    public bool death;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();

        activeNum = -1;
        //StartCoroutine(ActiveSelector());
    }

    // Update is called once per frame
    void Update()
    {

        if (!death)
        {
            Move();
            Turn();

        }



        //transform.position += moveVelocity * movePower * Time.deltaTime;



    }




    public void Move()
    {
        Vector3 activeVelocity = new Vector3(activeNum, 0, 0);
        transform.position += activeVelocity * moveSpeed * Time.deltaTime;
    }

    public void Turn()
    {
        Vector2 frontVec = new Vector2(transform.position.x + activeNum * 0.7f, transform.position.y);
        //적의 진행방향을 기준으로 앞의 벡터 값 정의(와 말 간지난다 지렷다)
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector2.down, 0.5f, LayerMask.GetMask("DDANG"));
        //Raycast 함수를 호출해서 frontVec 값을 대입한 뒤 해당 좌표에 Ground Layer를 가진 객체가 있는지 확인한 뒤 bool 값을 반환함

        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0)); 
        // raycast로 낭떠러지 계산하는 line 출력

        if (rayHit.collider)
            //만약 raycast로 호출한 rayHit 값이 true라면 
        {
            activeNum *= -1;
            //적의 이동 vector 값 중 x값을 반전시킨다
        }
       
    }


    IEnumerator ActiveSelector()
    {

        yield return new WaitForSeconds(2f);
        activeNum *= -1;

        StartCoroutine(ActiveSelector());

    }

    IEnumerator PTD()
    {

        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);

    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player" && col.transform.position.y > transform.position.y + 0.2f)
            //col의 tag가 Player일 때 && col의 y축 위치 값이 이 오브젝트의 y축 위치 값보다 클 때
        {
            anim.SetBool("Die", true);
            death = true;
            StartCoroutine(PTD());

        }

        else if(col.gameObject.tag == "Player")
        {

        }

    }


}
