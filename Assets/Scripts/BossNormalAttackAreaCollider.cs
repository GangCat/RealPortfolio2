using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormalAttackAreaCollider : BossAttackAreaCollider
{
    protected void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            _other.GetComponent<PlayerCollider>().TakeDmg(dmg);
        }
        myCollider.enabled = false;
        gameObject.SetActive(false);
    }
}
