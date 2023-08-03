using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandSaw : EnemyWeaponBase
{
    public override void OnAttack()
    {
        anim.Play("Punch", -1, 0);
        enemyCollider.Attack(1f);
    }

    public override void Reload()
    {
    }

    private void Start()
    {
        enemyCollider.Setup(weaponSetting.dmg);
        limitDistance = weaponSetting.attackDistance * 1.5f;
    }

    [SerializeField]
    private EnemyCollider enemyCollider = null;
    [SerializeField]
    private Animator anim = null;
}
