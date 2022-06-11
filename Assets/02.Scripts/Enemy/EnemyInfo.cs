using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyInfo : Entity_Enemy
{

    protected EnemyState[] states;
    protected EnemyState curruntStates;

    public int Level;

    public override float MaxHP => data.HP + HP_RoundValue;// * Round;
    protected float HP_RoundValue;

    public override float MaxMP => data.MP + MP_RoundValue;
    protected float MP_RoundValue;

    //public override float MaxSP => data.SP + SP_RoundValue;
    //protected float SP_RoundValue;

    public override float HPRecovery => data.HPRecovery + HPRecovery_RoundValue;
    protected float HPRecovery_RoundValue;
  

    public override float MPRecovery => data.MPRecovery + MPRecovery_RoundValue;
    protected float MPRecovery_RoundValue;


    //public override float SPRecovery => data.SPRecovery + SPRecovery_Item + SPRecovery_Buff;
    //protected float SPRecovery_Item;
    //protected float SPRecovery_Buff;


    public override float DP => data.DP + DP_RoundValue;
    protected float DP_RoundValue;

    public override float MoveSpeed => data.moveSpeed + MoveSpeed_RoundValue + MoveSpeed_Buff;
    protected float MoveSpeed_RoundValue;
    protected float MoveSpeed_Buff;

    public override float Damage => data.attackDamage + Damage_Item + Damage_Buff;
    protected float Damage_Item;
    protected float Damage_Buff;


    public override float HitRecoveryTime => data.hitRecovery + HitRecovery_Buff; // data.List[enemyNum].knockBacktime +
    protected float HitRecovery_Buff;


    public LayerMask whatIsGrounded;


    private float[] skillCoolTime = new float[2];




    public override void TakeDamage(float damage, float stiffness)
    {
        //HP -= damage; //±âÁ¸ÄÚµå
        HP -= Mathf.Clamp((damage * -1) + DP, 0, HP);
        StartCoroutine(HitAnimation());
        StartCoroutine(HitRecovery(stiffness));
    }


    private IEnumerator HitAnimation()
    {
        Color color = sprite.color;

        color.a = 0.2f;
        sprite.color = color;

        yield return new WaitForSeconds(0.3f);

        color.a = 1;
        sprite.color = color;
    }




    public override IEnumerator HitRecovery(float time)
    {

        time = HitRecoveryTime + time;
        yield return new WaitForSecondsRealtime(time);
        rb2d.velocity = Vector3.zero;
    }

    public override void Updated()
    {
        Debug.Log(curruntStates);
        curruntStates.Execute(this);
    }

    public override void LookChange()
    {
        //Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (MaxHP <= 0)
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


    public override void SetUp()
    {
        states = new EnemyState[2];
        states[(int)EnemyStates.idle] = new EnemyOwnedStates.Idle();
        states[(int)EnemyStates.Move] = new EnemyOwnedStates.Move();


        //ChangeState(PlayerStates);
        HP = MaxHP;
        MP = MaxMP;
        //SP = MaxSP;
        if (HPRecovery > 0)
            StartCoroutine(Recovery());

        slide.hp.maxValue = HP;
        slide.hp.value = HP;
        ChangeState(EnemyStates.idle);

    }

    public void ChangeState(EnemyStates newState)
    {

        Debug.Log("ChangeState: " + newState);
        //새로 바꾸려는 상태가 없으면 상태를 바꾸지 않음
        if (states[(int)newState] == null)
            return;

        // 현재 재생중인 상태의 Exit 호출 
        if (curruntStates != null)
            curruntStates.Exit(this);

        //새로운 상태로 변경하고 새로바뀐 상태의 Enter 호출
        curruntStates = states[(int)newState];
        curruntStates.Enter(this);
    }

    public override void SetEngage()
    {
        thePlayer.engageCounter++;
        thePlayer.isCombat = true;
        base.isCombat = true;



    }

    public override void GetOffEngage()
    {
        thePlayer.engageCounter--;
        if (thePlayer.engageCounter == 0)
            thePlayer.isCombat = false;

        base.isCombat = false;
    }





    //딜레이 없이 공격 상태변화 가능 약 > 강 // 강 > 특공 등 ... 
}
