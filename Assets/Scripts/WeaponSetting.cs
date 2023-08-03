using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum EAttackType { None = -1, Melee, Range}

[System.Serializable]
public struct WeaponSetting
{
    public EAttackType attackType;
    public float dmg; // ���ݷ�
    public float maxDmg; // �ִ� ���ݷ�
    public int curAmmo; // ���� ź�� ��
    public int maxAmmo; // �ִ� ź�� ��
    public float attackRate; // ���� �ӵ�
    public float attackDistance; // ���� ��Ÿ�
    public bool isAutomaticAttack; // ���� ���� ���� ����
}