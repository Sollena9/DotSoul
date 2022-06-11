using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStates { idle = 0, Move, Jump, Air, Grabed, Skill, Guard, Countered, Hit, Groggy };


public abstract class Entity_Enemy : MonoBehaviour
{

    public PlayerData data;

    //HP 정보  0 ~ MaxHp 사이 값을 넘어 갈 수 없도록 설정
    public float HP
    {
        set => data.HP = Mathf.Clamp(value, 0, MaxHP);
        get => data.HP;
    }
    //MP 정보  0 ~ MaxMp 사이 값을 넘어 갈 수 없도록 설정
    public float MP
    {
        set => data.MP = Mathf.Clamp(value, 0, MaxMP);
        get => data.MP;
    }
    //public float SP
    //{
    //    set => data.SP = Mathf.Clamp(value, 0, MaxSP);
    //    get => data.SP;
    //}

    //추상 클래스로 정의하여 파생 클래스에서 정의
    public abstract float MaxHP { get; }
    public abstract float HPRecovery { get; }
    public abstract float MaxMP { get; }
    public abstract float MPRecovery { get; }
    //public abstract float MaxSP { get; }
    //public abstract float SPRecovery { get; }

    public abstract float MoveSpeed { get; }
    public abstract float Damage { get; }

    public abstract float DP { get; }
    public abstract float HitRecoveryTime { get; }


    public bool isRightFace;
    public bool isGrounded;
    public bool isCombat;
    public bool isAggro;
    public float curruntEngegeTime = 0f;

    [SerializeField]
    protected SliderManager slide;
    [SerializeField]
    protected SpriteRenderer sprite;
    [SerializeField]
    protected Rigidbody2D rb2d;
    [SerializeField]
    public Animator anim;
    [SerializeField]
    public Player_Movement movement;
    [SerializeField]
    protected PlayerCooltimeManager cooltimeManager;
    [SerializeField]
    protected PlayerInfo thePlayer;



    protected IEnumerator Recovery()
    {
        while (true)
        {
            if (HP < MaxHP) HP += HPRecovery;
            //if (MP < MaxMP) MP += MPRecovery;
            //if (SP < MaxSP) SP += SPRecovery; // 전투 중 회복 X 추가해야됨
            yield return new WaitForSeconds(1f);
        }
    }

    public abstract void TakeDamage(float damage, float stiffness);
    //public abstract void PrintDamageText(float damage);

    public abstract IEnumerator HitRecovery(float time);


    public abstract void Updated();


    public abstract void LookChange();

    public abstract void SetUp();

    public abstract void SetEngage();

    public abstract void GetOffEngage();

}
