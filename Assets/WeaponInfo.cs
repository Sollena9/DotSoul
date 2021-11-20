using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor.Examples;
#endif


[CreateAssetMenu(fileName = "PlayerWeapon", menuName = "DotSoul/PlayerWeapon")]
[InlineEditor]
public class EnemyData : SerializedScriptableObject
{


    [BoxGroup("Basic Info")]
    [LabelWidth(100)]
    [PropertyTooltip("무기 이름")]
    public string enemyName;

    [LabelWidth(100)]
    [TextArea]
    [BoxGroup("Basic Info")]
    [PropertyTooltip("무기 설명")]
    public string description;

    [BoxGroup("Basic Info")]
    [LabelWidth(100)]
    [PropertyTooltip("무기 번호")]
    public int weaponNum;


    [HorizontalGroup("Game Data", 150)]
    [PreviewField(100)]
    [HideLabel]
    [PropertyTooltip("게임오브젝트 0: 본체 / 1: AttPosition / 2: 체력바 / 3: 히트 이펙트")]
    public GameObject weaponDropItem;


    [VerticalGroup("Game Data/Stats")]
    [ProgressBar("Min", "Max", r: 1f, g: 0, b: 0, Height = 30)]
    [PropertyTooltip("공격력")]
    public int attackPoint;
    private int Min = 0;
    public int Max;




    [ProgressBar(0, 40, r: 0.15f, g: 0.17f, b: 0.74f, Height = 30)]
    [PropertyTooltip("이동 속도")]
    public int movePower;

    [ProgressBar(0, 100, r: 0f, g: 0f, b: 0f, Height = 30)]
    [PropertyTooltip("색적 시야")]
    public int detectRange;

    [ProgressBar(0, 100, r: 1f, g: 1f, b: 1f, Height = 30)]
    [PropertyTooltip("공격 사정거리")]
    public int attackRange;

    [ProgressBar(0, 10, r: 1f, g: 0.2f, b: 0.3f, Height = 30)]
    [PropertyTooltip("공격력")]
    public int attackDamage;

    [ProgressBar(0, 100, 0, 0.5f, 0.5f, Height = 30)]
    [PropertyTooltip("공격 쿨타임")]
    public float attackCoolTime;


    [VerticalGroup("Enemy Setting")]
    [ProgressBar(0, 3, 0, 0, 0, Height = 30)]
    [PropertyTooltip("뒤로돌아 밸류 값")]
    public float groundCheckValue;

    [ProgressBar(0, 10, r: 1f, g: 0.2f, b: 0.3f, Height = 30)]
    [PropertyTooltip("소환 이펙트 크기")]
    public float spawnEffectSize = 1;

    [ColorPalette("파티클 팔레트")]
    public Color dieColor;
    public string attackName;
    public Material hit;
    public float flashDelay = 0.1f;


    [ProgressBar(0, 100, r: 0.15f, g: 0.47f, b: 0, Height = 30)]
    [PropertyTooltip("드랍 골드 개수")]
    public int dropGoldCount;

    [PropertyTooltip("데미지 폰트 Y축 위치")]
    public float fontPositionY;



}