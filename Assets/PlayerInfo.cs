using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int Level;

    public float[] statArray = new float[5];
    //여기서부터 순서대로

    public float HP;
    public float FP;
    public float SP;
    public float movePower;
    public float jumpPower;
    public int jumpcounter;
    public float dodgePower;

    public int estusMaxCount; // 최대 소유 가능한 에스트 HP+MP = 최대 개ㅅ
    public int estusHPCurruntCount;
    public int estusMPCurruntCount;

 
    public float AP; //공격 데미지 계수
    public float MP; //마법 데미지

    public float Soul;

    public float DP; //Defense Point 그냥 방어력임

    public float guardMovePower; // 가드 시 이동속도 감소되는 수치

    public LayerMask whatIsGrounded;
    public bool isGrounded;
   
    public float[] skillCoolTime = new float[2];

    public bool eqipWeapon;
    public bool eqipShield;



    //레벨업 시 스탯 증가 계수- HP, FP, SP, AP, MP, DP 순서
    public float[] increaseStat = new float[6];

    //public GameObject hp;
    //public GameObject fp;
    //public GameObject sp;
    //HP 구현


    public void PlayerLevelUp(float levNeedSoul)
    {
        Soul -= levNeedSoul;

        Level++;
        HP += RandomAblityPointCalculator();
        FP += RandomAblityPointCalculator();
        SP += RandomAblityPointCalculator();
        AP += RandomAblityPointCalculator();
        MP += RandomAblityPointCalculator();
        DP += RandomAblityPointCalculator();

        //이펙트 출력

    }



    private float RandomAblityPointCalculator()
    {
        var ablity = Random.Range(Level / 100 * 2 + 3, Level / 100 * 2 - 3);

        return ablity; 
    }

    // 5일때 100/현재레벨 * 2 = 40
    // 50일때 100/현재레벨 * 2 = 4
    // 80일때 100/현재레벨 * 2 = 2




}
