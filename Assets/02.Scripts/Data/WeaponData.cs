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
public class WeaponData : SerializedScriptableObject
{

    [VerticalGroup("Basic Info")]
    [PreviewField(150)]
    [HideLabel]
    [PropertyTooltip("이미지")]
    [OnInspectorInit("@Texture = EditorIcons.OdinInspectorLogo")]
    //[OnInspectorGUI("DrawPreview", append: true)] //프리뷰 설명임
    public Texture2D Texture;

    /*private void DrawPreview()
    {
        if (this.Texture == null) return;

        GUILayout.BeginVertical(GUI.skin.box);
        GUILayout.Label(this.Texture);
        GUILayout.EndVertical();
    }*/
    
    [VerticalGroup("Basic Info")] // 스탯 별 카테고리
    [LabelWidth(100)] // 스탯 좌우 길이
    [PropertyTooltip("무기 이름")] // 툴팁
    public string weaponName; // 변수명

    [VerticalGroup("Basic Info")]
    [LabelWidth(100)]
    [PropertyTooltip("무기 번호")]
    public int weaponNum;

    [LabelWidth(100)]
    [TextArea]
    [VerticalGroup("Basic Info")]
    [PropertyTooltip("무기 설명")]
    public string description;






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