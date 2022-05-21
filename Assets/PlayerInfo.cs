using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpriteTrail;

public class PlayerInfo : Entity
{

    
    
    public int Level;

    public override float MaxHP => data.HP + HP_Item + HP_Buff;
    protected float HP_Item;
    protected float HP_Buff;

    public override float MaxMP => data.MP + MP_Item + MP_Buff;
    protected float MP_Item;
    protected float MP_Buff;

    public override float MaxSP => data.SP + SP_Item + SP_Buff;
    protected float SP_Item;
    protected float SP_Buff;

    public override float HPRecovery => data.HPRecovery + HPRecovery_Item + HPRecovery_Buff;
    protected float HPRecovery_Item;
    protected float HPRecovery_Buff;

    public override float MPRecovery => data.MPRecovery + MPRecovery_Item + MPRecovery_Buff;
    protected float MPRecovery_Item;
    protected float MPRecovery_Buff;


    public override float SPRecovery => data.SPRecovery + SPRecovery_Item + SPRecovery_Buff;
    protected float SPRecovery_Item;
    protected float SPRecovery_Buff;


    public override float DP => data.DP + DP_Item + DP_Buff;
    protected float DP_Item;
    protected float DP_Buff;

    public override float MoveSpeed => data.moveSpeed + MoveSpeed_Item + MoveSpeed_Buff;
    protected float MoveSpeed_Item;
    protected float MoveSpeed_Buff;

    public override float Damage => data.attackDamage + Damage_Item + Damage_Buff;
    protected float Damage_Item;
    protected float Damage_Buff;


    public override float HitRecoveryTime => data.hitRecovery + HitRecovery_Item + HitRecovery_Buff; // data.List[enemyNum].knockBacktime +
    protected float HitRecovery_Item;
    protected float HitRecovery_Buff;


    public LayerMask whatIsGrounded;
    public bool isGrounded;
   
    private float[] skillCoolTime = new float[2];


    protected int jumpCounter;
    [SerializeField]
    protected SpriteTrail.SpriteTrail[] trails;




    //레벨업 시 스탯 증가 계수- HP, FP, SP, AP, MP, DP 순서
    public float[] increaseStat = new float[6];


    public void PlayerLevelUp(int levNeedSoul)
    {
        data.soul -= levNeedSoul;

        Level++;
        //HP_Max += RandomAblityPointCalculator();
        //FP += RandomAblityPointCalculator();
        SP += RandomAblityPointCalculator();
        //AP += RandomAblityPointCalculator();
        MP += RandomAblityPointCalculator();
        //DP += RandomAblityPointCalculator();

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




    public override void TakeDamage(float damage)
    {
        //HP -= damage; //±âÁ¸ÄÚµå
        HP -= Mathf.Clamp((damage * -1) + DP, 0, HP);
        StartCoroutine(HitAnimation());
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

    private void Awake()
    {
        SetUp();
    }


    public override IEnumerator HitRecovery(float time)
    {

        time = HitRecoveryTime + time;
        yield return new WaitForSecondsRealtime(time);
        rb2d.velocity = Vector3.zero;
    }


}
