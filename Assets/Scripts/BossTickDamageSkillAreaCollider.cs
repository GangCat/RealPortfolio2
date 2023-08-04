using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossTickDamageSkillAreaCollider : BossAttackAreaCollider
{
    protected virtual void Update()
    {
        lastDamageTime += Time.deltaTime;
    }

    protected virtual void OnEnable()
    {
        lastDamageTime = 1.0f;
    }

    protected void OnTriggerStay(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            if (lastDamageTime >= damageTickTime)
            {
                lastDamageTime = 0.0f;
                _other.GetComponent<PlayerCollider>().TakeDmg(dmg);
            }
        }
    }


    [SerializeField]
    private float damageTickTime = 0.5f;

    private float lastDamageTime = 0.0f;
}
