using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDamageBuff : ItemBase
{
    public override void Use(GameObject _entity)
    {
        if(_entity.GetComponentInChildren<WeaponAssaultRifle>() != null)
            _entity.GetComponentInChildren<WeaponAssaultRifle>().ChangeDmg(changeDmgRatio, duration);
        Destroy(gameObject);
    }


    [SerializeField]
    private float changeDmgRatio = 1.0f;
    [SerializeField]
    private float duration = 10f;
}
