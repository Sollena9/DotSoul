using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor.Examples;
#endif




[CreateAssetMenu(fileName = "Player", menuName = "DotSoul/PlayerData")]
[InlineEditor]
public class PlayerData : SerializedScriptableObject
{

    [EnumToggleButtons, HideLabel]
    public playerCombatState playerState;

    [EnumToggleButtons, HideLabel]
    public playerWeaponState weaponState;



    public enum playerWeaponState
    {
        Fist,// 맨손
        One, // 한손
        Two // 두손
    }

    [EnumToggleButtons, HideLabel]
    public enum playerCombatState
    {
        Idle, Jump ,Walk, Attack_Normal, Guard, Attack_Strike, Skill, Hit, Estus, Roll, Counter, Grab, Die,
        Freeze
    }

    [HideInInspector]
    public int weaponStateCount;


    [VerticalGroup("Basic Info")]

    public string playerName;
    public float HP; // 체력
    public float MP; // 마력 
    public float SP; // 스태미너
    public float HPRecovery;
    public float MPRecovery;
    public float SPRecovery;


    public float DP; // 방어력 
    public float AP; // 데미지 계수
    public int level; 
    public float exp;

    public int hitRecovery;
    public float moveSpeed;
    public float moveSpeed_Guard;
    public float jumpPower;
    public int attackSpeed;
    public float rollPower;

    public int estusMaxCount;
    public int estusHPCurruntCount;
    public int estusMPCurruntCount;

    public int soul;

    public float coolTime_Roll;


    [PropertyTooltip("무기 히트박스 크기")]
    public Vector2 hitBoxValue;

    [PropertyTooltip("무기 히트박스 위치")]
    public Transform hitBoxPosition;


    [ProgressBar(0, 10, r: 1f, g: 0.2f, b: 0.3f, Height = 30)]
    [PropertyTooltip("공격력")]
    public int attackDamage;



}