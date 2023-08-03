using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackAreaCollider : MonoBehaviour
{
    public void OnAttack(float _delayTime)
    {
        attackAreaCollider.enabled = true;
        Invoke("AttackFinish", _delayTime);
    }

    private void AttackFinish()
    {
        attackAreaCollider.enabled = false;
    }


    private void Awake()
    {
        attackAreaCollider = GetComponent<Collider>();
    }

    private void Start()
    {
        attackAreaCollider.enabled = false;
    }

    private Collider attackAreaCollider = null;
}
