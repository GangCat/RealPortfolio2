using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponMinigun : EnemyWeaponBase
{
    public override void OnAttack()
    {
        Vector3 targetPos = targetTr.position;
        targetPos.y = 1.0f;
        Quaternion rot = Quaternion.LookRotation(targetPos - trMuzzleOfWeapon.position);
        projectileMemoryPool.SpawnProjectile(trMuzzleOfWeapon.position, rot, weaponSetting.dmg);
        anim.Play("Shoot", -1, 0);

        --weaponSetting.curAmmo;
    }

    public override void Reload()
    {
        weaponSetting.curAmmo = weaponSetting.maxAmmo;
    }


    private void Start()
    {
        limitDistance = weaponSetting.attackDistance * 1.5f;
    }

    private void Awake()
    {
        projectileMemoryPool = GetComponent<ProjectileMemoryPool>();
    }


    [SerializeField]
    private Transform trMuzzleOfWeapon = null;
    [SerializeField]
    private Animator anim = null;

    private ProjectileMemoryPool projectileMemoryPool = null;
}
