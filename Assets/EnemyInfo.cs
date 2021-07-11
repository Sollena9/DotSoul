using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    public enum State
    {
        Idle, Walk, Follow, Attack, Parry, Groggy, Hit, Grabbed, Skill, Die
    }

    public State enemyState;

    public int enemyLevel;
    // 0: 일반 몬스터 , 1: 보스




}
