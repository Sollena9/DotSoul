using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor.Examples;
#endif



[CreateAssetMenu(fileName = "Enemy", menuName = "DotSoul/EnemyData")]
[InlineEditor]
public class EnemyData : SerializedScriptableObject
{

    public class Item
    {
        public GameObject item;
        public float dropPossibility;
    }


    public class Skill
    {
        public int skillDamage;
        public float coolTime;
        public float skillHitRecovery;
    }

    public enum EnemyGrade
    {
        Jaco,// 잡몹 
        Boss_Middle, // 중간보스 
        Boss_Stage // 스테이지보스 
    }

    public enum EnemyDivision
    {
        Melee, // 근접 
        Range, // 원거리 
        Magical // 마법 
    }


    [VerticalGroup("Basic Info")]

    public string enemyName;
    public string enemyExplain;
    public float HP; // 체력
    public float MP; // 마력 
    public float SP; // 스태미너
    public float DP; // 방어력 
    public float HPRecovery;
    public float MPRecovery;
    public float SPRecovery;

    public float waitTime; // 다음 행동을 위해 대기(idle)하는 시간 
    public int level;
    public float exp;
    public int dropSoul;
    public Dictionary<int, Item> dropItems = new Dictionary<int, Item>();

    public float roundValue;



    public int hitRecovery;
    public float moveSpeed;
    public int attackSpeed;


    public int boss_MaxPhase = 0; // 최대 페이즈 
    public int curruntPhase;      // 현재 페이즈 
    public float maxRagePoint;    // 최대 분노량
    public float[] rageSection;   // 분노 구간 
    public float curruntRagePoint;// 현재 분노량

    public bool isBattleStart = false; // 등장 연출을 위한 불 값 


    [ProgressBar(0, 10, r: 1f, g: 0.2f, b: 0.3f, Height = 30)]
    [PropertyTooltip("기본 공격력")]
    public int attackDamage;


    public Dictionary<int, Skill> skills = new Dictionary<int, Skill>(); // 스킬 데미지 


}