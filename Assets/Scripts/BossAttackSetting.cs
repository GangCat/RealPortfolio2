using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BossAttackSetting
{
    [Header("-Attack & Skill")]
    [SerializeField]
    public int closeRangeAttackCount;
    [SerializeField]
    public int longRangeAttackCount;
    [SerializeField]
    public int closeRangeSkillCount;
    [SerializeField]
    public int longRangeSkillCount;
    [SerializeField]
    public float skillDelayTime;

    [Header("-Attack Range")]
    [SerializeField]
    public float closeRangeAttackRange;
    [SerializeField]
    public float longRangeAttackRange;

    [Header("-Has Attack Type")]
    [SerializeField]
    public bool hasCloseRangeAttack;
    [SerializeField]
    public bool hasLongRangeAttack;
    [SerializeField]
    public bool hasCloseRangeSkill;
    [SerializeField]
    public bool hasLongRangeSkill;
}
