using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    public bool hitPlayer;
    public int selectNum;


    //화톳불 ON/OFF 체크
    public bool fireState;

    private void Start()
    {
        selectNum = 0;
    }

    private void Update()
    {
        // 레벨업 키 반응 코드
        if (hitPlayer && Input.GetKeyDown(KeyCode.E) && !fireState)
        {
            //animation 전환
            //anim.setbool("Fire", true);
        }
        else if (Input.GetKeyDown(KeyCode.E) && hitPlayer && fireState)
        {
            //메뉴출력
            Rest();
        }


        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (selectNum == 0)
                return;

            else
            {
                selectNum--;
                UIManager game = FindObjectOfType<UIManager>();
                game.MoveBonfireSeletor();
            }
        }

        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (selectNum == 2)
                return;

            else
            {
                selectNum++;
                UIManager game = FindObjectOfType<UIManager>();
                game.MoveBonfireSeletor();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // 플레이어 닿았을 때 로직
        if (col.gameObject.CompareTag("Player"))
        {
            hitPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        // 플레이어 닿지 않았을 때 로직
        if (col.gameObject.CompareTag("Player"))
        {
            hitPlayer = false;
            UIManager ui = FindObjectOfType<UIManager>();
            ui.ShowBonFireUI(0);


        }
    }


    private void Rest()
    {

        UIManager ui = FindObjectOfType<UIManager>();

        ui.bonfire = this.gameObject;

        ui.ShowBonFireUI(1);
    }






}
